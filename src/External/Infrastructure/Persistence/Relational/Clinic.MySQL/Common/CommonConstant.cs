using System.Collections.Generic;

namespace Clinic.MySQL.Common;

/// <summary>
///     Represent set of constant.
/// </summary>
internal static class CommonConstant
{
    internal static class Database
    {
        internal static class DataType
        {
            internal const string DATETIME = "DATETIME";

            internal const string TEXT = "TEXT";

            /// <summary>
            ///     Varchar datatype resolver.
            /// </summary>
            internal static class VarcharGenerator
            {
                private static readonly Dictionary<int, string> _storage = [];
                private const string varcharDataTypeName = "VARCHAR";

                /// <summary>
                ///     Generate Varchar datatype with given length.
                /// </summary>
                /// <param name="length">
                ///     Length of Varchar.
                /// </param>
                /// <returns>
                ///     The old instance if it is already existed
                ///     or the new one.
                /// </returns>
                /// <remarks>
                ///     The extension cannot generate max length.
                /// </remarks>
                internal static string Get(int length)
                {
                    if (_storage.TryGetValue(key: length, value: out var value))
                    {
                        return value;
                    }

                    var newValue = $"{varcharDataTypeName}({length})";

                    _storage.Add(key: length, value: newValue);

                    return newValue;
                }
            }
        }

        public static class Collation
        {
            public const string UTF8MB4_BIN = "utf8mb4_bin";
        }
    }
}
