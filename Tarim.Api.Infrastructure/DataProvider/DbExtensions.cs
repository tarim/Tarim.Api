using System;
using System.Data;

namespace Tarim.Api.Infrastructure.DataProvider
{
    internal static class DbExtensions
    {
        public static string GetString(this IDataReader reader, string columnName)
        {
            return GetValueOrNull(reader, columnName, reader.GetString);
        }
        public static string GetString(this IDataReader reader, string columnName, string defaultValue)
        {
            return GetValueOrDefault(reader, columnName, reader.GetString, defaultValue);
        }
        public static double? GetNullableDouble(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetDouble);
        }

        public static float? GetNullableFloat(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetFloat);
        }

        public static long? GetNullableLong(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetInt64);
        }

        public static int? GetNullableInt(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetInt32);
        }

        public static Int16? GetNullableShort(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetInt16);
        }

        public static decimal? GetNullableDecimal(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetDecimal);
        }

        public static DateTime? GetNullableDateTime(this IDataReader reader, string columnName)
        {
            return GetNullable(reader, columnName, reader.GetDateTime);
        }

        public static T GetEnum<T>(this IDataReader reader, string columnName)
        {
            return (T)Enum.Parse(typeof(T), reader.GetString(columnName), true);
        }
        public static long GetLong(this IDataReader reader, string columnName)
        {
            return GetValueOrDefault(reader, columnName, reader.GetInt64, 0);
        }

        public static int GetInt(this IDataReader reader, string columnName)
        {
            return GetValueOrDefault(reader, columnName, reader.GetInt32, 0);
        }

        public static Boolean GetBoolean(this IDataReader reader, string columnName)
        {
            return GetValueOrDefault(reader, columnName, reader.GetBoolean, false);
        }
        private static byte[] GetBlob(this IDataReader reader, int i)
        {
            var dataReaderWrapper = reader as DataReaderWrapper;
            if (dataReaderWrapper != null)
                return dataReaderWrapper.GetBlob(i);
            throw new NotSupportedException();
        }

        public static byte[] GetBlob(this IDataReader reader, string columnName)
        {
            return GetValueOrNull(reader, columnName, reader.GetBlob);
        }

        public static bool FieldExists(this IDataReader reader, string columnName)
        {
            var i = reader.GetOrdinal(columnName);
            return i >= 0;
        }
        #region utility methods
        private static T DoInTryCatch<T>(Func<T> action, T defaultValue)
        {
            try
            {
                return action();
            }
            catch (IndexOutOfRangeException)
            {
                return defaultValue;
            }
        }
        private static T DoInTryCatch<T>(Func<T> action) where T : class
        {
            return DoInTryCatch(action, null);
        }
        private static T? DoInTryCatch<T>(Func<T?> action) where T : struct
        {
            try
            {
                return action();
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        private static T GetValueOrNull<T>(IDataReader reader, string columnName, Func<int, T> action) where T : class
        {
            return DoInTryCatch(() =>
            {
                var i = reader.GetOrdinal(columnName);
                return i < 0 || reader.IsDBNull(i) ? (T)null : action(i);
            });
        }

        private static T GetValueOrDefault<T>(IDataReader reader, string columnName, Func<int, T> action, T defaultValue)
        {
            return DoInTryCatch(() =>
            {
                var i = reader.GetOrdinal(columnName);
                return i < 0 || reader.IsDBNull(i) ? defaultValue : action(i);
            }, defaultValue);
        }

        private static T? GetNullable<T>(IDataReader reader, string columnName, Func<int, T> action) where T : struct
        {
            return DoInTryCatch(() =>
            {
                var i = reader.GetOrdinal(columnName);
                return i < 0 || reader.IsDBNull(i) ? (T?)null : action(i);
            });
        }
        #endregion
    }
}
