using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Settings;

namespace IllTechLibrary.Serialization
{
    public class Deserialize<T>
    {
        public Deserialize(String TableName)
        {
            table = TableName;

            typeList = new List<Type>();
            nameList = new List<string>();
            valueList = new List<object>();

            // Deserialize object into types members and values
            T instance = (T)Activator.CreateInstance(typeof(T));

            List<FieldInfo> info = instance.GetType().GetFields().ToList();

            memberCount = info.Count;

            for (int i = 0; i < memberCount; i++)
            {
                if (AttributeMethods.Validate<LocaleAttribute>(info[i], Core.LangCode) == ValidateState.Remove)
                {
                    info.RemoveAt(i);
                    memberCount = info.Count;
                    i--;
                    continue;
                }

                if (AttributeMethods.Validate<RequiredVersion>(info[i], Preferences.TargetVersion()) == ValidateState.Remove)
                {
                    info.RemoveAt(i);
                    memberCount = info.Count;
                    i--;
                    continue;
                }

                typeList.Add(info[i].FieldType);
                nameList.Add(info[i].Name);

                if (typeList[i] == typeof(String))
                {
                    valueList.Add(info[i].GetValue(instance));
                }
                else
                {
                    valueList.Add(Activator.CreateInstance(typeList[i]));
                }
            }

            WhereValue = String.Empty;
        }

        // Changed from void type to resulting type
        public Deserialize<T> SetData(T instance)
        {
            List<FieldInfo> info = instance.GetType().GetFields().ToList();

            memberCount = info.Count;

            for (int i = 0; i < memberCount; i++)
            {
                if (AttributeMethods.Validate<LocaleAttribute>(info[i], Core.LangCode) == ValidateState.Remove)
                {
                    info.RemoveAt(i);
                    memberCount = info.Count;
                    i--;
                    continue;
                }

                if (AttributeMethods.Validate<RequiredVersion>(info[i], Preferences.TargetVersion()) == ValidateState.Remove)
                {
                    info.RemoveAt(i);
                    memberCount = info.Count;
                    i--;
                    continue;
                }

                if (Attribute.IsDefined(info[i], typeof(AliasAttribute)))
                {
                    valueList[i] = (bool)info[i].GetValue(instance) == true ? 1 : 0;
                    continue;
                }

                if (typeList[i] == typeof(string) && info[i].GetValue(instance) == null)
                {
                    valueList[i] = String.Empty;
                    continue;
                }

                valueList[i] = info[i].GetValue(instance);
            }

            return this;
        }

        public T Serialize()
        {
            return (T)Activator.CreateInstance(typeof(T), valueList);
        }

        // Type by name and index
        public Type GetType(int index) { return typeList[index]; }
        public Type GetType(String name) { return typeList[nameList.FindIndex(p => p.Equals(name))]; }

        // Get Name By Index
        public String GetName(int index) { return nameList[index]; }

        // Get the database name list
        public string GetNames()
        {
            string names = string.Empty;

            for (int i = 0; i < nameList.Count; i++)
            {
                if (typeList[i] == typeof(string))
                {
                    names += $"IFNULL({nameList[i]}, \'\')";
                }
                else
                {
                    names += nameList[i];
                }

                if (i < nameList.Count - 1)
                    names += ", ";
            }

            return names;
        }

        // Get Index By String (-1 If Does Not Exist)
        public int GetNameIndex(String name)
        {
            return nameList.FindIndex(p => p.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        // Set value by index and by name
        public void SetValue(string name, object data) { valueList[nameList.FindIndex(p => p.Equals(name))] = data; }
        public void SetValue(int index, object data) { valueList[index] = data; }
        public void SetValues(object[] data) { valueList = data.ToList(); }

        // For changing the SQL conditions
        public void SetConditions(String con) { conditions = con; }
        public String GetConditions() { return conditions == null ? String.Empty : conditions; }

        public void SetWhere(Object p)
        {
            this.WhereValue = Convert.ToString(p);
        }

        // What we order the enrty by
        public void SetKey(String key) { this.key = key; }
        public String GetKey() { return key; }

        // To string method to satisfy C# when implementing == and !=
        public override string ToString()
        {
            string toStr = string.Empty;

            for (int i = 0; i < nameList.Count; i++)
            {
                toStr += $"{nameList[i]}={Convert.ToString(valueList[i])}";

                if (i < nameList.Count - 1)
                    toStr += ", ";
            }

            return $"({toStr})";
        }

        // Hash function because C# asked for one
        public override int GetHashCode()
        {
            int hashCode = 0;

            for (int i = 0; i < memberCount; i++)
            {
                hashCode = (hashCode << i % 4) ^ nameList[i].First();
            }

            return Math.Abs(hashCode);
        }

        // Comparators
        public override bool Equals(object obj)
        {
            if (!(obj is Deserialize<T>) && !(obj is T)) return false;

            if(obj is Deserialize<T>)
            {
                return this == (Deserialize<T>)obj;
            }
            else
            {
                return this == (T)obj;
            }
        }

        public static bool operator==(Deserialize<T> left, Deserialize<T> right)
        {
            // Right away we dont have matching sizes
            if (left.memberCount != right.memberCount)
                return false;

            // Compare each member type first then value
            for(int i = 0; i < left.memberCount; i++)
            {
                if (left.typeList[i] != right.typeList[i])
                    return false;

                if (left[i].ToString() != right[i].ToString())
                    return false;
            }

            return true;
        }

        public static bool operator !=(Deserialize<T> left, Deserialize<T> right)
        {
            return !(left == right);
        }

        public static bool operator ==(Deserialize<T> left, T right)
        {
            return left == new Deserialize<T>("NaN").SetData(right);
        }

        public static bool operator !=(Deserialize<T> left, T right)
        {
            return left != new Deserialize<T>("NaN").SetData(right);
        }

        // Array Accessors
        public object this[String name]
        {
            get { return valueList[nameList.FindIndex(p => p.Equals(name))]; }
            set { valueList[nameList.FindIndex(p => p.Equals(name))] = value; }
        }

        public object this[int index]
        {
            get { return valueList[index]; }
            set { valueList[index] = value; }
        }

        // The table primary key
        public String key;
        public String WhereValue;

        // If this class needs conditions to go with its Query
        public String conditions;

        public String table;
        public int memberCount;
        public List<Type> typeList;
        public List<String> nameList;
        public List<Object> valueList;
    }
}
