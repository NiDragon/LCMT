using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using MySql.Data.MySqlClient;
using IllTechLibrary.Settings;
using IllTechLibrary.Util;
using IllTechLibrary.Localization;
using System.Data;

namespace IllTechLibrary
{
    /// <summary>
    /// Locking Mechanism for Asyncronous Database Transactions.
    /// </summary>
    public class DatabaseLock : IDisposable
    {
        private IllSQL connection;

        /// <summary>
        /// Construct a new database locking mechanism.
        /// </summary>
        /// <param name="connection">The connection to lock</param>
        public DatabaseLock(IllSQL connection)
        {
#if AUTH_SERVICE
            NetSession.Instance().Pulse();
#endif

            this.connection = connection;
            connection.KeepAliveCheck(null, null);
            connection.AcquireLock();
        }

        /// <summary>
        /// Release all resources tied to this locking context.
        /// </summary>
        public void Dispose()
        {
            connection.ReleaseLock();
        }

        /// <summary>
        /// Static conversion to MySqlConnection.
        /// </summary>
        /// <param name="left">The Lock Instance</param>
        public static implicit operator MySqlConnection(DatabaseLock left)
        {
            return left.connection.sqlcon;
        }

        /// <summary>
        /// Gets the valid state of the connection object.
        /// </summary>
        /// <returns>Ture if the connection is not null.</returns>
        public bool IsValid()
        {
            bool valid = connection.IsValid();

            // because the transaction functions only return instead of alerting you when this fails
            // we can give some helpful info before it returns on failure
            if (!valid)
            {
                try
                {
                    throw new InvalidOperationException("MySQL Connection State Not Valid!");
                }
                catch(Exception mysqlex) 
                { 
                    // Not sure if this works but could be helpful for debuggin random failed connections
                    // This being useful depends on how many previous frames of stack we can report
                    MsgDialogs.Show("Transaction Error",
                        $"MySql Read Error {CException.GetStackNoException()}\n{mysqlex.Message}",
                        "ok", MsgDialogs.MsgTypes.ERROR);
                }
            }

            return valid;
        }
    }

    /// <summary>
    /// An abstracted database connection.
    /// </summary>
    public class IllSQL
    {
        private bool ShuttingDown = false;
        private System.Timers.Timer PulseCheck = null;
        private SpinLock PulseLock = new SpinLock();

        public MySqlConnection sqlcon;
        private String conString;

        private int lockCount = 0;

        public Action<string, string, string, MsgDialogs.MsgTypes> OnConnectionLost;

        /// <summary>
        /// Default connection constructor
        /// </summary>
        ~IllSQL()
        {
            if (PulseCheck != null)
            {
                PulseCheck.Close();
                PulseCheck.Dispose();

                PulseCheck = null;
            }
        }

        /// <summary>
        /// Trys to verify MySQL Server and Credentials.
        /// </summary>
        /// <param name="Server">MySQL Host</param>
        /// <param name="User">MySQL User</param>
        /// <param name="Pwd">MySQL Password</param>
        /// <param name="Port">MySQL Port</param>
        /// <returns></returns>
        public bool ConnectTest(String Server, String User, String Pwd, String Port)
        {
            try
            {
#if AUTH_SERVICE
                NetSession.Instance().Pulse();
#endif

                conString = String.Format("server={0};uid={1}; pwd={2}; port={3};",
                    Server, User, Pwd, Port);

                sqlcon = new MySqlConnection(conString);

                sqlcon.Open();

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Connect this connection to the designated host.
        /// </summary>
        /// <param name="ConString">The host connection string</param>
        /// <param name="ReqLock">Should we use locking</param>
        /// <returns></returns>
        public bool Connect(String ConString, bool ReqLock = true)
        {
            ShuttingDown = false;

            if (IsOpenOrConnecting())
            {
                return true;
            }

            bool connect = false;

            try
            {
#if AUTH_SERVICE
                NetSession.Instance().Pulse();
#endif
                if (ReqLock)
                    AcquireLock();

                conString = ConString;
                sqlcon = new MySqlConnection(conString);

                sqlcon.InfoMessage += ReportSqlMessage;

                sqlcon.Open();

                if (PulseCheck == null)
                {
                    PulseCheck = new System.Timers.Timer(5000);
                    PulseCheck.AutoReset = true;
                    PulseCheck.Enabled = true;
                    PulseCheck.Elapsed += KeepAliveCheck;
                }

                connect = true;
            }
            catch (Exception)
            {
                // Ignore exception object for now might need it in the future.
                MsgDialogs.LogError($"MySql Connect Failed: {sqlcon.Database} On {sqlcon.DataSource}");
            }
            finally
            {
                if(ReqLock)
                    ReleaseLock();
            }

            return connect;
        }

        /// <summary>
        /// Report anything MySql might have to say.
        /// </summary>
        /// <param name="sender">MySqlConnection</param>
        /// <param name="args">The messages sent to the connection.</param>
        private void ReportSqlMessage(object sender, MySqlInfoMessageEventArgs args)
        {
            foreach (MySqlError er in args.errors) {
                MsgDialogs.LogWarning($"MySqlMessage[{er.Code}][{er.Level}] : {er.Message}");
            }
        }

        /// <summary>
        /// Attempt to keep connection alive for a long time.
        /// This might be theoreticaly useless and we should just reopen the connection.
        /// Further investigation for this is required.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeepAliveCheck(object sender, EventArgs e)
        {
            if(sqlcon != null)
            {
                Exception except = null;

                try
                {
                    // Aquire Lock Check For Life
                    AcquireLock();

                    // Blocking Call?
                    if (!sqlcon.Ping())
                    {
                        // Blocking Call?
                        sqlcon.Open();

                        if(PulseCheck == null)
                        {
                            PulseCheck = new System.Timers.Timer(5000);
                            PulseCheck.AutoReset = true;
                            PulseCheck.Enabled = true;
                            PulseCheck.Elapsed += KeepAliveCheck;
                        }
                    }
                }
                catch (Exception ex)
                {
                    PulseCheck.Dispose();
                    PulseCheck = null;

                    except = ex;
                }
                finally
                {
                    // Release Lock
                    ReleaseLock();

                    if (except != null)
                    {
                        OnConnectionLost?.Invoke("Keep Alive Thread", except.Message, "OK", MsgDialogs.MsgTypes.ERROR);
                    }
                }
            }
        }

        /// <summary>
        /// Disconnect this connection and notify all existing attempts to use this connection.
        /// </summary>
        public void Disconnect()
        {
            ShuttingDown = true;

            if (IsConnected())
            {
                try
                {
                    AcquireLock();

                    sqlcon.Close();
                    sqlcon.Dispose();
                    sqlcon = null;

                    PulseCheck.Close();
                    PulseCheck.Dispose();
                    PulseCheck = null;
                }
                catch (Exception)
                {
                    sqlcon.Dispose();
                    sqlcon = null;

                    PulseCheck.Dispose();
                    PulseCheck = null;
                }
                finally
                {
                    ReleaseLock();
                }
            }
            else
            {
                try
                {
                    AcquireLock();

                    if (sqlcon != null)
                    {
                        sqlcon.Dispose();
                        sqlcon = null;
                    }

                    if (PulseCheck != null)
                    {
                        PulseCheck.Dispose();
                        PulseCheck = null;
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    ReleaseLock();
                }
            }
        }

        /// <summary>
        /// Check to see if this connection is ready to do any work.
        /// </summary>
        /// <returns>Connection Ready</returns>
        public bool IsConnected()
        {
            try
            {
                if (sqlcon != null)
                {
                    return (sqlcon.State != ConnectionState.Closed &&
                        sqlcon.State != ConnectionState.Broken &&
                        sqlcon.State != ConnectionState.Connecting);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is the state of this device changing?
        /// </summary>
        /// <returns>Is the connection active</returns>
        private bool IsOpenOrConnecting()
        {
            try
            {
                if (sqlcon != null)
                {
                    return (sqlcon.State != ConnectionState.Closed &&
                        sqlcon.State != ConnectionState.Broken);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Switch selected database table associated with this connection.
        /// </summary>
        /// <param name="newDB">The name of the table to swtich to.</param>
        public bool SwitchDB(String newDB)
        {
            bool success = true;

            if (IsConnected())
            {
                AcquireLock();

                try
                {
                    sqlcon.ChangeDatabase(newDB);
                }
                catch (Exception)
                {
                    success = false;
                }
                finally
                {
                    ReleaseLock();
                }

                return success;
            }

            return success;
        }

        /// <summary>
        /// Get the table name associated with a specific connection.
        /// </summary>
        /// <param name="DbName">The Connection Name</param>
        /// <returns>Table Name For Connection</returns>
        public static String GetToolDB(String DbName)
        {
            FileIniDataParser i = new FileIniDataParser();

            i.Parser.Configuration.CommentString = "#";

            IniData data = i.ReadFile(Preferences.GetConfig());

            return data["DS"][DbName];
        }

        /// <summary>
        /// Switch the tool database.
        /// </summary>
        /// <param name="DbName">The DB we are changing</param>
        /// <param name="val">The new name of the DB</param>
        public static void SetToolDB(String DbName, String val)
        {
            FileIniDataParser i = new FileIniDataParser();

            i.Parser.Configuration.CommentString = "#";
            i.Parser.Configuration.CaseInsensitive = true;

            IniData data = i.ReadFile(Preferences.GetConfig());

            data["DS"][DbName] = val;

            Preferences.WriteData(data);
        }

        /// <summary>
        /// Acquire the lock of the connection on current thread.
        /// </summary>
        public void AcquireLock()
        {
            bool refLock = false;

            if (!PulseLock.IsHeldByCurrentThread)
            {
                PulseLock.Enter(ref refLock);
                lockCount++;
            }
            else
            {
                lockCount++;
            }
        }

        /// <summary>
        /// Release the lock state of the connection.
        /// </summary>
        public void ReleaseLock()
        {
            // Current Thread Released Last Lock
            if(--lockCount == 0)
                PulseLock.Exit();
        }

        /// <summary>
        /// Check if this connection is locked up
        /// </summary>
        /// <returns></returns>
        public bool IsLocked()
        {
            return PulseLock.IsHeld;
        }

        /// <summary>
        /// Gets the valid state of the connection object.
        /// </summary>
        /// <returns>Ture if the connection is not null.</returns>
        public bool IsValid()
        { 
            if(sqlcon != null && PulseCheck != null && !ShuttingDown)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
