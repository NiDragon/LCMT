#define _PATCH_PARALLEL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllTechLibrary.Serialization;
using MySql.Data.MySqlClient;
using System.IO;
using IllTechLibrary.Util;
using System.Runtime.InteropServices;
using System.Threading;
using IllTechLibrary.Settings;
using IniParser.Model;
using System.Diagnostics;
using System.Xml.Serialization;

namespace IllTechLibrary
{
    public enum QUERY_TYPE
    {
        UPDATE,
        INSERT,
        DELETE
    }

    /// <summary>
    /// Reflection Database Adapter Class.
    /// Note: Functions now abort when connection is null.
    /// </summary>
    /// <typeparam name="T">Database Type</typeparam>
    public class Transactions<T>
    {
        private IllSQL theCon;

        /// <summary>
        /// Default Construction Requires a Data Connection.
        /// </summary>
        /// <param name="Connection">Database Connection Object</param>
        public Transactions(IllSQL Connection)
        {
            theCon = Connection;
        }

        /// <summary>
        /// Execute a query return all results that match the criteria.
        /// </summary>
        /// <param name="classData">Deserialized Structure Object</param>
        /// <returns>List of returned objects</returns>
        public List<T> ExecuteQuery(Deserialize<T> classData)
        {
            List<T> ret = new List<T>();

            // Execute MySql command from class data
#if _PATCH_PARALLEL
            String Query = $"SELECT COUNT(*) as TotalRows FROM {classData.table} {classData.GetConditions()};SELECT {classData.GetNames()} FROM {classData.table} {classData.GetConditions()}";
#else
            String Query = $"SELECT {classData.GetNames()} FROM {classData.table} {classData.GetConditions()}";
#endif

            using (DatabaseLock ctx = new DatabaseLock(theCon))
            {
                if (!ctx.IsValid())
                    return ret;

                MySqlCommand command = new MySqlCommand(Query, ctx)
                {
                    CommandTimeout = 2147483
                };

                using (MySqlDataReader dr = command.ExecuteReader())
                {
#if _PATCH_PARALLEL
                    int indexer = 0;

                    if (dr.HasRows)
                    {
                        try
                        {
                            dr.Read();

                            T[] rowData = new T[int.Parse(dr[0].ToString())];

                            dr.NextResult();

                            if (dr.HasRows)
                            {
                                object objectLocker = false;

                                ParallelOptions po = new ParallelOptions();
                                po.MaxDegreeOfParallelism = Environment.ProcessorCount;

                                CancellationTokenSource token = new CancellationTokenSource();
                                po.CancellationToken = token.Token;

                                Parallel.For(0, rowData.Count(), po, (px, state) =>
                                {
                                // If we encountered an exception and must break out of the loop
                                if (po.CancellationToken.IsCancellationRequested)
                                        state.Break();

                                // Should really be no way for dr to have different row counts than classdata
                                object[] objs = new object[classData.memberCount];

                                    lock (objectLocker)
                                    {
                                        dr.Read();

                                        for (int i = 0; i < dr.FieldCount; i++)
                                        {
                                            if (dr[i].GetType() != typeof(DBNull))
                                            {
                                                if (dr[i].GetType() == typeof(String))
                                                {
                                                    objs[i] = Prepare(dr[i].ToString());
                                                }
                                                else
                                                {
                                                    objs[i] = dr[i];
                                                }
                                            }
                                            else
                                            {
                                                objs[i] = Activator.CreateInstance(classData[i].GetType());
                                            }
                                        }
                                    }

                                    try
                                    {
                                        Deserialize<T> classObj = new Deserialize<T>(classData.table);

                                        classObj.SetValues(objs);

                                        rowData[px] = classObj.Serialize();

                                        Interlocked.Increment(ref indexer);
                                    }
                                    catch (Exception e)
                                    {
                                        MsgDialogs.Show("Transaction Error",
                                            $"Setting Value Failed {CException.GetLastFrame(e, true)}",
                                            "ok", MsgDialogs.MsgTypes.ERROR);

                                        token.Cancel();
                                    }
                                });

                                while (indexer != rowData.Count() && !token.IsCancellationRequested)
                                    Thread.Sleep(10);

                                ret.AddRange(rowData);
                            }
                        } 
                        catch(Exception mysqlex)
                        {
                            MsgDialogs.Show("Transaction Error",
                                $"MySql Read Error {CException.GetLastFrame(mysqlex, true)}",
                                "ok", MsgDialogs.MsgTypes.ERROR);
                        }
                    }
#else
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            for (int i = 0; i < classData.memberCount; i++)
                            {
                                // Catch invalid database layouts
                                try
                                {
                                    if (dr[i].GetType() != typeof(DBNull))
                                    {
                                        classData.SetValue(i, dr[i]);
                                    }
                                    else
                                    {
                                        if (classData.typeList[i] == typeof(String))
                                        {
                                            classData.SetValue(i, String.Empty);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }

                            ret.Add(classData.Serialize());
                        }
                    }
#endif
                    dr.Close();
                    dr.Dispose();
                    command.Dispose();
                }
            }

            return ret;
        }

        public string Prepare(String value, bool load = true)
        {
            if(Preferences.EP3Transcode())
            {
                Encoding target = Core.Encoder;
                Encoding single = Encoding.GetEncoding("windows-1252");
                Encoding source = Encoding.UTF8;

                if (load)
                {
                    byte[] win1252Bytes = Encoding.Convert(
                        source, single, source.GetBytes(value));

                    return target.GetString(win1252Bytes);
                }
                else
                {
                    return single.GetString(target.GetBytes(value));
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Execute single query UPDATE/DELETE/INSERT
        /// </summary>
        /// <param name="classData">Deserialized Structure Object</param>
        /// <param name="type">Type of query to execute</param>
        public void ExecuteQuery(Deserialize<T> classData, QUERY_TYPE type)
        {
            StringBuilder sb = new StringBuilder();

            string where = ""; 
            
            if(classData.key != string.Empty || classData.WhereValue != string.Empty)
                where = classData.WhereValue == String.Empty ? classData[classData.key].ToString() : classData.WhereValue;

            switch (type)
            {
                case QUERY_TYPE.UPDATE:
                    if (classData.GetConditions() == String.Empty)
                    {
                        sb.Append($"UPDATE `{classData.table}` SET %data WHERE {classData.key}={where};");
                    }
                    else
                    {
                        sb.Append($"UPDATE `{classData.table}` SET %data {classData.GetConditions()}");
                    }
                    break;
                case QUERY_TYPE.INSERT:
                    sb.Append($"INSERT INTO `{classData.table}` (%data0) VALUES (%data1);");
                    break;
                case QUERY_TYPE.DELETE:
                    if (classData.GetConditions() == String.Empty)
                    {
                        sb.Append($"DELETE FROM `{classData.table}` WHERE {classData.key}={where};");
                    }
                    else
                    {
                        sb.Append($"DELETE FROM `{classData.table}` {classData.GetConditions()}");
                    }
                    break;
                default:
                    break;
            }

            String values = "";
            String values2 = "";

            for (int i = 0; i < classData.memberCount; i++)
            {
                switch (type)
                {
                    case QUERY_TYPE.UPDATE:
                        values += String.Format("{0}='{1}'", classData.GetName(i), Prepare(classData[i].ToString().Replace(@"'", @"''"), false));
                        if (i + 1 != classData.memberCount)
                            values += ",";
                        break;
                    case QUERY_TYPE.INSERT:
                        values += String.Format("'{0}'", Prepare(classData[i].ToString().Replace(@"'", @"''"), false));
                        values2 += String.Format("{0}", classData.GetName(i));
                        if (i + 1 != classData.memberCount)
                        {
                            values += ",";
                            values2 += ",";
                        }
                        break;
                    case QUERY_TYPE.DELETE:
                        break;
                    default:
                        break;
                }
            }

            if (type == QUERY_TYPE.INSERT)
            {
                sb = sb.Replace("%data0", values2).Replace("%data1", values);
            }
            else
            {
                sb = sb.Replace("%data", values);
            }
#if DEBUG
            TextWriter tw = new StreamWriter(File.Open("Testing.txt", FileMode.Create));
            tw.Write(sb.ToString());
            tw.Flush();
            tw.Close();
            tw.Dispose();
#else
            try
            {
                using (DatabaseLock ctx = new DatabaseLock(theCon))
                {
                    if (!ctx.IsValid())
                        return;

                    sb = sb.Replace(@"\", "\\\\").Replace("/", "\\\\");
                    using (MySqlCommand command = new MySqlCommand(sb.ToString(), ctx))
                    {
                        command.CommandTimeout = 2147483;
                        command.Prepare();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception mysqlex)
            {
                MsgDialogs.Show("Transaction Error",
                    $"MySql Read Error {CException.GetLastFrame(mysqlex, true)}",
                    "ok", MsgDialogs.MsgTypes.ERROR);
            }
#endif
        }

        /// <summary>
        /// Execute multiple UPDATE/DELETE/INSERT queries at once
        /// </summary>
        /// <param name="classData">Deserialized Structure Object</param>
        /// <param name="data">List of data objects</param>
        /// <param name="type">Type of query to execute</param>
        public void ExecuteQuery(Deserialize<T> classData, List<T> data, QUERY_TYPE type)
        {
            StringBuilder QList = new StringBuilder();

            bool cancel = false;

            for (int i = 0; i < data.Count; i++)
            {
                classData.SetData(data[i]);

                String temp = StartQuery(type, classData);
                String values = "";
                String values2 = "";

                // Guessing String.Format was used to make it easier to read in this case
                // Cannot remember wrote it so long ago
                for (int j = 0; j < classData.memberCount; j++)
                {
                    switch (type)
                    {
                        case QUERY_TYPE.UPDATE:
                            values += $"{classData.GetName(j)}='{Prepare(classData[j].ToString().Replace(@"'", @"''"), false)}'";
                            if (j + 1 != classData.memberCount)
                                values += ",";
                            break;
                        case QUERY_TYPE.INSERT:
                            values += $"'{Prepare(classData[j].ToString().Replace(@"'", @"''"), false)}'";
                            values2 += $"{classData.GetName(j)}";
                            if (i + 1 != classData.memberCount)
                            {
                                values += ",";
                                values2 += ",";
                            }
                            break;
                        case QUERY_TYPE.DELETE:
                            break;
                        default:
                            break;
                    }
                }

                if (type == QUERY_TYPE.INSERT)
                {
                    temp = temp.Replace("%data0", values2).Replace("%data1", values);
                }
                else
                {
                    temp = temp.Replace("%data", values);
                }

                QList.Append(temp.Replace(@"\", "\\\\").Replace("/", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n"));
                QList.Append(" _ILLTEK_ ");
            }

#if DEBUG
            QList = QList.Replace(" _ILLTEK_ ", Environment.NewLine);
            TextWriter tw = new StreamWriter(File.Open("Testing.txt", FileMode.Create));
            tw.Write(QList);
            tw.Flush();
            tw.Close();
            tw.Dispose();
#else
            String[] splitList = QList.ToString().Split(new string[] { " _ILLTEK_ " }, StringSplitOptions.RemoveEmptyEntries);

            // split command switch
            if (splitList.Count() > 400)
            {
                List<string> queue = new List<string>();

                // 200 query limit per call
                for (int i = 0; i < splitList.Count(); i++)
                {
                    String TempQ = String.Empty;

                    for (int j = 0; j < 200; j++)
                    {
                        if ((i * 200 + j) > (splitList.Count() - 1))
                            break;

                        TempQ += splitList[i * 200 + j] + "\n";
                    }

                    if (TempQ == String.Empty)
                        break;

                    queue.Add(TempQ);
                }

                using (DatabaseLock ctx = new DatabaseLock(theCon))
                {
                    if (!ctx.IsValid())
                        return;

                    // Begin a transaction
                    MySqlConnection con = ctx;
                    MySqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    // Task error will return false prompting rollback
                    Task<bool> Exec = new Task<bool>(
                            () =>
                            {
                                for (int i = 0; i < queue.Count; i++)
                                {
                                    if (cancel)
                                        return false;

                                    try
                                    {
                                        using (MySqlCommand command = new MySqlCommand(queue[i], ctx))
                                        {
                                            command.CommandTimeout = 2147483;
                                            command.Prepare();
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                    catch (Exception exr)
                                    {
                                        MsgDialogs.Show("Database Error!",
                                            $"MySQL query failed database has not been modified!\n\n{exr.Message}",
                                            "ok", MsgDialogs.MsgTypes.ERROR);
                                        return false;
                                    }
                                }

                                return true;
                            });

                    Exec.Start();

                    while (!cancel && !Exec.Wait(1))
                    {
                        cancel = Interrupt.AbortRequested.GetStatus();
                        Thread.Sleep(1);
                    }

                    if (cancel || !Exec.Result)
                    {
                        transaction.Rollback();
                        Interrupt.AbortRequested.Reset();

                        MsgDialogs.ShowNoLog("Debug Message!", "This is a succesful test of the rollback function!", "ok", MsgDialogs.MsgTypes.INFO);

                        return;
                    }
                    else
                        transaction.Rollback();

                    transaction.Dispose();
                }
            }
            else
            {
                using (DatabaseLock ctx = new DatabaseLock(theCon))
                {
                    if (!ctx.IsValid())
                        return;

                    // Begin a transaction
                    MySqlConnection con = ctx;
                    MySqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    // Task error will return false prompting rollback
                    Task<bool> Exec = new Task<bool>(
                            () =>
                            {
                                try
                                {
                                    using (MySqlCommand command = new MySqlCommand(QList.ToString(), ctx))
                                    {
                                        command.CommandTimeout = 2147483;
                                        command.Prepare();
                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception exr)
                                {
                                    MsgDialogs.Show("Database Error!",
                                        $"MySQL query failed database has not been modified!\n\n{exr.Message}",
                                        "ok", MsgDialogs.MsgTypes.ERROR);
                                    return false;
                                }

                                if (cancel)
                                {
                                    return false;
                                }
                                else { return true; }
                            });

                    Exec.Start();

                    while (!cancel && !Exec.Wait(1))
                    {
                        cancel = Interrupt.AbortRequested.GetStatus();
                        Thread.Sleep(1);
                    }

                    if (cancel || !Exec.Result)
                    {
                        transaction.Rollback();
                        Interrupt.AbortRequested.Reset();

                        MsgDialogs.ShowNoLog("Debug Message!", "This is a succesful test of the rollback function!", "ok", MsgDialogs.MsgTypes.INFO);

                        return;
                    }
                    else
                        transaction.Rollback();

                    transaction.Dispose();
                }
            }
#endif
        }

        /// <summary>
        /// Define a header for a new query string.
        /// </summary>
        /// <param name="type">The Type of Query</param>
        /// <param name="classData">The Deserialized Structure</param>
        /// <returns></returns>
        private String StartQuery(QUERY_TYPE type, Deserialize<T> classData)
        {
            String ret = "";
            String where = "";
            
            if(classData.key != string.Empty || classData.WhereValue != string.Empty)
                where = classData.WhereValue == String.Empty ? classData[classData.key].ToString() : classData.WhereValue;

            switch (type)
            {
                case QUERY_TYPE.UPDATE:
                    ret += $"UPDATE `{classData.table}` SET %data WHERE {classData.key}={where};";
                    break;
                case QUERY_TYPE.INSERT:
                    ret += $"INSERT INTO `{classData.table}` (%data0) VALUES (%data1);";
                    break;
                case QUERY_TYPE.DELETE:
                    ret += $"DELETE FROM `{classData.table}` WHERE {classData.key}={where};";
                    break;
                default:
                    throw new NotImplementedException("Unknown Query Type!");
            }

            return ret;
        }

        /// <summary>
        /// Query number of effected records.
        /// </summary>
        /// <param name="Query">Command Text</param>
        /// <returns>0 On Fail</returns>
        public int ExecuteScalar(String Query)
        {
            int ret = 0;

            using (DatabaseLock ctx = new DatabaseLock(theCon))
            {
                if (!ctx.IsValid())
                    return 0;

                using (MySqlCommand command = new MySqlCommand(Query, ctx))
                {
                    command.CommandTimeout = 2147483;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            ret += 1;

                        while (reader.NextResult())
                        {
                            ret += 1;
                        }

                        reader.Close();
                        reader.Dispose();
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Execute a non return raw query.
        /// </summary>
        /// <param name="Query">Command Text</param>
        public void ExecuteQuery(String Query)
        {
            using (DatabaseLock ctx = new DatabaseLock(theCon))
            {
                if (!ctx.IsValid())
                    return;
#if DEBUG
                TextWriter tw = new StreamWriter(File.Open("Testing.txt", FileMode.Create));
                tw.Write(Query);
                tw.Flush();
                tw.Close();
                tw.Dispose();
#else
                using (MySqlCommand command = new MySqlCommand(Query, ctx))
                {
                    command.CommandTimeout = 2147483;
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
#endif
            }
        }

        /// <summary>
        /// Get integer value of specific column from query.
        /// </summary>
        /// <param name="Query">Command Text</param>
        /// <param name="Col">Column Name</param>
        /// <returns>-1 If value not found</returns>
        public T GetInt(String Query, String Col)
        {
            T ret = Activator.CreateInstance<T>();

            // Exclusive Lock
            using (DatabaseLock ctx = new DatabaseLock(theCon))
            {
                if (!ctx.IsValid())
                    return ret;

                using (MySqlCommand command = new MySqlCommand(Query, ctx))
                {
                    command.CommandTimeout = 2147483;
                    command.Prepare();

                    using (MySqlDataReader dr = command.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            ret = (T)Convert.ChangeType(dr[Col], typeof(T));
                        }

                        dr.Close();
                        dr.Dispose();
                    }
                }
            }

            return ret;
        }
    }
}
