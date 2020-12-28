using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCMT.Common
{
    enum SharedDataClasses
    {
        eALL,
        eITEMS
    }

    enum DataInterfaceExchange
    {
        eINSERT,
        eCHANGE,
        eREMOVE,
        eRELOAD
    }

    internal class SyncObject<T>
    {
        private readonly T _internal;

        private SyncObject(T other)
        {
            _internal = other;
        }

        public static implicit operator T(SyncObject<T> b) => b._internal;
        
        public static implicit operator SyncObject<T>(T other) 
        {
            SyncObject<T> newObject = new SyncObject<T>(other);

            SharedData.PushNotify<T>(other, DataInterfaceExchange.eCHANGE);
            
            return newObject; 
        }
    }

    internal class SharedData
    {
        // Tuple Who - Subscribed To What
        private static List<Tuple<LCToolFrm, SharedDataClasses>> m_subs;

        private static SynchronizedCollection<IllTechLibrary.SharedStructs.Item> _Items;

        /// <summary>
        /// A single instance of tool form item data
        /// </summary>
        internal static SynchronizedCollection<IllTechLibrary.SharedStructs.Item> Items
        {
            get { return _Items; }
            set { _Items = value; NotifyAll(SharedDataClasses.eITEMS); }
        }

        internal static void PushNotify<T>(T obj, DataInterfaceExchange change)
        {
            SyncObject<IllTechLibrary.SharedStructs.Item> stm = new IllTechLibrary.SharedStructs.Item();

            stm = new IllTechLibrary.SharedStructs.Item();
        }

        /// <summary>
        /// Notify all subscribers to this list of a reload
        /// </summary>
        /// <param name="type">the type reloaded</param>
        private static void NotifyAll(SharedDataClasses type)
        {
            lock(m_subs)
            {
                foreach(Tuple<LCToolFrm, SharedDataClasses> frm in m_subs.FindAll(p=>p.Item2.Equals(type)))
                {
                    // Notify of a reload because that should be the only time this value is set
                }
            }
        }

        /// <summary>
        /// Subscribe to a shared data objects update events
        /// </summary>
        /// <param name="form">the form subscribing</param>
        /// <param name="type">the type of data to subscribe to</param>
        internal static void Subscribe(LCToolFrm form, SharedDataClasses type)
        {
            if (type != SharedDataClasses.eALL)
            {
                int idx = m_subs.FindIndex(p => p.Item1.GetToolID() == form.GetToolID());

                if (idx == -1)
                {
                    lock (m_subs)
                    {
                        m_subs.Add(new Tuple<LCToolFrm, SharedDataClasses>(form, type));
                    }
                }
            }
        }

        /// <summary>
        /// Unsubscribe from a shared data objects update events
        /// </summary>
        /// <param name="form">the form unsubscribing</param>
        /// <param name="type">the type of data to unsub from eALL for everything</param>
        internal static void Unsubcribe(LCToolFrm form, SharedDataClasses type)
        {
            // Attempt to remove all elements for this tool form
            if (type == SharedDataClasses.eALL)
            {
                lock(m_subs)
                {
                    m_subs.RemoveAll(p => p.Item1.GetToolID() == form.GetToolID());
                }
            }
            else
            {
                int idx = m_subs.FindIndex(p => p.Item1.GetToolID() == form.GetToolID() && p.Item2.Equals(type));

                // If we have an index remove this single element
                if (idx != -1)
                {
                    lock (m_subs)
                    {
                        m_subs.RemoveAt(idx);
                    }
                }
            }
        }
    }
}
