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
            public string Sample;
            public TypeMap(SqlDbType sqlDbType, Type type, string sample)
            {
                this.Type = type;
                this.SqlDbType = sqlDbType;
                this.Sample = sample;
            }
        };

        private static Random Random = new Random();
        private static ArrayList _TypeList = new ArrayList();

        static TypeConvertor()
        {
            _TypeList.Add(new TypeMap(SqlDbType.BigInt, typeof(System.Int64), Random.Next(int.MinValue, int.MaxValue).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.Binary, typeof(System.Byte[]), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Bit, typeof(System.Boolean), Random.Next(2) == 0 ? "true" : "false"));
            _TypeList.Add(new TypeMap(SqlDbType.Char, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Date, typeof(System.DateTime), "DateTime.Now"));
            _TypeList.Add(new TypeMap(SqlDbType.DateTime, typeof(System.DateTime), "DateTime.Now"));
            _TypeList.Add(new TypeMap(SqlDbType.DateTime2, typeof(System.DateTime), "DateTime.Now"));
            _TypeList.Add(new TypeMap(SqlDbType.DateTimeOffset, typeof(System.DateTimeOffset), "DateTime.Now"));
            _TypeList.Add(new TypeMap(SqlDbType.Decimal, typeof(System.Decimal), Random.Next(0, 128).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.Float, typeof(System.Double), (1.0f / Random.NextDouble()).ToString("#.##d", CultureInfo.InvariantCulture)));
            _TypeList.Add(new TypeMap(SqlDbType.Int, typeof(System.Int32), Random.Next(int.MinValue, int.MaxValue).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.Image, typeof(System.Byte[]), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Money, typeof(System.Decimal), Random.Next(0, 128).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.NChar, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.NText, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.NVarChar, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Real, typeof(System.Single), (1.0f / Random.NextDouble()).ToString("#.##f", CultureInfo.InvariantCulture)));
            _TypeList.Add(new TypeMap(SqlDbType.SmallDateTime, typeof(System.DateTime), "DateTime.Now"));
            _TypeList.Add(new TypeMap(SqlDbType.SmallInt, typeof(System.Int16), Random.Next(0, 128).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.SmallMoney, typeof(System.Decimal), Random.Next(0, 128).ToString()));
            _TypeList.Add(new TypeMap(SqlDbType.Text, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Time, typeof(System.TimeSpan), "TimeSpan.FromDays(1)"));
            _TypeList.Add(new TypeMap(SqlDbType.Timestamp, typeof(System.Byte[]), ""));
            _TypeList.Add(new TypeMap(SqlDbType.TinyInt, typeof(System.Byte), "(byte)(" + Random.Next(0, 128).ToString() + ")"));
            _TypeList.Add(new TypeMap(SqlDbType.UniqueIdentifier, typeof(System.Guid), "Guid.NewGuid()"));
            _TypeList.Add(new TypeMap(SqlDbType.VarBinary, typeof(System.Byte[]), ""));
            _TypeList.Add(new TypeMap(SqlDbType.VarChar, typeof(System.String), ""));
            _TypeList.Add(new TypeMap(SqlDbType.Variant, typeof(System.Object), "null"));
        }

        private TypeConvertor()
        {
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

        public static TypeMap Find(string sqlDbType)
        {
            object retObj = null;
            for (int i = 0; i < _TypeList.Count; i++)
            {
                TypeMap entry = (TypeMap)_TypeList[i];
                if (entry.SqlDbType.ToString().Replace("SqlDbType.","").ToLower() == sqlDbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
                throw new Exception("TypeMapEntry:UnsupportedType:" + sqlDbType.ToString());

            return (TypeMap)retObj;
        }

        private static TypeMap Find(Type type)
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