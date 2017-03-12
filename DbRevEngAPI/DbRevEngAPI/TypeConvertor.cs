using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace DbRevEngAPI
{
    public sealed class TypeConvertor
    {

        public struct TypeMap
        {
            public Type Type;
            public SqlDbType SqlDbType;
            public string DbType;
            public TypeMap(string dbType, SqlDbType sqlDbType, Type type)
            {
                this.Type = type;
                this.SqlDbType = sqlDbType;
                this.DbType = dbType;
            }

            public string CsSampleData()
            {
                switch (this.Type.Name.ToString())
                {
                    case "Int64": return ((long)(Random.NextDouble() * Int64.MaxValue)).ToString();
                    case "Byte[]": return "new byte[] {" + Random.Next(0, 256).ToString() + "}";
                    case "Boolean": return (Random.NextDouble() >= 0.5).ToString().ToLower();
                    case "String": return "\"" + Guid.NewGuid().ToString().Substring(0, 1) + "\"";
                    case "DateTime": return "new DateTime(" + DateTime.Now.Ticks.ToString() + ")";
                    case "DateTimeOffset": return "new DateTimeOffset(new DateTime(" + DateTime.Now.Ticks.ToString() + "))";
                    case "Decimal": return string.Format("new decimal({0},{1},{2},{3},(byte)({4}))", Random.Next(Int32.MinValue, Int32.MaxValue), Random.Next(Int32.MinValue, Int32.MaxValue), Random.Next(Int32.MinValue, Int32.MaxValue), (Random.NextDouble() >= 0.5).ToString().ToLower(), Random.Next(0, 28));
                    case "Double": return (Random.NextDouble() * Double.MaxValue).ToString().Replace(",", ".") + "d";
                    case "Int32": return Random.Next(Int32.MinValue, Int32.MaxValue).ToString();
                    case "Byte": return "(byte)(" + Random.Next(0, 256).ToString() + ")";
                    case "Single": return (Random.NextDouble() * Single.MaxValue).ToString().Replace(",",".") + "f";
                    case "Int16": return Random.Next(Int16.MinValue, Int16.MaxValue).ToString();
                    case "TimeSpan": return "new TimeSpan(" + DateTime.Now.Ticks.ToString() + ")";
                    case "Guid": return "new Guid(\"" + Guid.NewGuid().ToString() + "\")";
                    case "Object": return "null";

                    default:
                        throw new Exception("CsSampleData:UnsupportedType:" + this.Type.ToString());
                }
            }
        };

        private static Random Random = new Random();
        private static ArrayList _TypeList = new ArrayList();

        static TypeConvertor()
        {
            _TypeList.Add(new TypeMap("bigint", SqlDbType.BigInt, typeof(System.Int64)));
            _TypeList.Add(new TypeMap("binary", SqlDbType.Binary, typeof(System.Byte[])));
            _TypeList.Add(new TypeMap("bit", SqlDbType.Bit, typeof(System.Boolean)));
            _TypeList.Add(new TypeMap("char", SqlDbType.Char, typeof(System.String)));
            _TypeList.Add(new TypeMap("date", SqlDbType.Date, typeof(System.DateTime)));
            _TypeList.Add(new TypeMap("datetime", SqlDbType.DateTime, typeof(System.DateTime)));
            _TypeList.Add(new TypeMap("datetime2", SqlDbType.DateTime2, typeof(System.DateTime)));
            _TypeList.Add(new TypeMap("datetimeoffset", SqlDbType.DateTimeOffset, typeof(System.DateTimeOffset)));
            _TypeList.Add(new TypeMap("decimal", SqlDbType.Decimal, typeof(System.Decimal)));
            _TypeList.Add(new TypeMap("float", SqlDbType.Float, typeof(System.Double)));
            _TypeList.Add(new TypeMap("int", SqlDbType.Int, typeof(System.Int32)));
            _TypeList.Add(new TypeMap("image", SqlDbType.Image, typeof(System.Byte[])));
            _TypeList.Add(new TypeMap("money", SqlDbType.Money, typeof(System.Decimal)));
            _TypeList.Add(new TypeMap("nchar", SqlDbType.NChar, typeof(System.String)));
            _TypeList.Add(new TypeMap("ntext", SqlDbType.NText, typeof(System.String)));
            _TypeList.Add(new TypeMap("numeric", SqlDbType.Decimal, typeof(System.Decimal)));
            _TypeList.Add(new TypeMap("nvarchar", SqlDbType.NVarChar, typeof(System.String)));
            _TypeList.Add(new TypeMap("real", SqlDbType.Real, typeof(System.Single)));
            _TypeList.Add(new TypeMap("rowversion", SqlDbType.Timestamp, typeof(System.Byte[])));
            _TypeList.Add(new TypeMap("smalldatetime", SqlDbType.SmallDateTime, typeof(System.DateTime)));
            _TypeList.Add(new TypeMap("smallint", SqlDbType.SmallInt, typeof(System.Int16)));
            _TypeList.Add(new TypeMap("smallmoney", SqlDbType.SmallMoney, typeof(System.Decimal)));
            _TypeList.Add(new TypeMap("structured", SqlDbType.Structured, typeof(System.Object)));
            _TypeList.Add(new TypeMap("text", SqlDbType.Text, typeof(System.String)));
            _TypeList.Add(new TypeMap("time", SqlDbType.Time, typeof(System.TimeSpan)));
            _TypeList.Add(new TypeMap("timestamp", SqlDbType.Timestamp, typeof(System.Byte[])));
            _TypeList.Add(new TypeMap("tinyint", SqlDbType.TinyInt, typeof(System.Byte)));
            _TypeList.Add(new TypeMap("uniqueidentifier", SqlDbType.UniqueIdentifier, typeof(System.Guid)));
            _TypeList.Add(new TypeMap("udt", SqlDbType.Udt, typeof(System.Object)));
            _TypeList.Add(new TypeMap("varbinary", SqlDbType.VarBinary, typeof(System.Byte[])));
            _TypeList.Add(new TypeMap("varchar", SqlDbType.VarChar, typeof(System.String)));
            _TypeList.Add(new TypeMap("sql_variant", SqlDbType.Variant, typeof(System.Object)));
            _TypeList.Add(new TypeMap("xml", SqlDbType.Xml, typeof(System.Object)));
        }

        private TypeConvertor()
        {
        }

        public static TypeMap Find(string dbType)
        {
            object retObj = null;
            for (int i = 0; i < _TypeList.Count; i++)
            {
                TypeMap entry = (TypeMap)_TypeList[i];
                if (entry.DbType == dbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
                throw new Exception("TypeMapEntry:UnsupportedType:" + dbType);

            return (TypeMap)retObj;
        }

        public static TypeMap Find(SqlDbType sqlDbType)
        {
            object retObj = null;
            for (int i = 0; i < _TypeList.Count; i++)
            {
                TypeMap entry = (TypeMap)_TypeList[i];
                if (entry.SqlDbType == sqlDbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
                throw new Exception("TypeMapEntry:UnsupportedType:" + sqlDbType.ToString());

            return (TypeMap) retObj;
        }

        public static TypeMap Find(Type type)
        {
            object retObj = null;
            for (int i = 0; i < _TypeList.Count; i++)
            {
                TypeMap entry = (TypeMap)_TypeList[i];
                if (entry.Type == (Nullable.GetUnderlyingType(type) ?? type))
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
                throw new Exception("TypeMapEntry:UnsupportedType:" + type.ToString());

            return (TypeMap)retObj;
        }
    }
}