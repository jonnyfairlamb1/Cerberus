using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CommonData.Extensions {

    public static class OracleExtensions {

        public static async Task<OracleDataReader> GetDataReaderAsync(this OracleConnection connection, string procedureName, OracleParameter[]? parameters = null) {
            if (connection.State != ConnectionState.Open) await connection.OpenAsync().ConfigureAwait(false);

            OracleParameter cursor = new("results", OracleDbType.RefCursor, ParameterDirection.Output);

            //create the procedure command
            await using OracleCommand command = connection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;

            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
            command.Parameters.Add(cursor);

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            var refCursor = cursor.Value as OracleRefCursor ?? null;
            if (refCursor == null) throw new InvalidOperationException("Unable to obtain data from the database due to an error");
            OracleDataReader reader = refCursor.GetDataReader();

            return reader;
        }

        public static async Task<int> SafeGetIntAsync(this DbDataReader reader, string columnName, int defaultValue = 0) {
            if (!await reader.IsDBNullAsync(columnName))
                return reader.GetInt32(columnName);
            return defaultValue;
        }

        public static async Task<string> SafeGetStringAsync(this DbDataReader reader, string columnName, string defaultValue = "") {
            if (!await reader.IsDBNullAsync(columnName))
                return reader.GetString(columnName);
            return defaultValue;
        }
    }
}