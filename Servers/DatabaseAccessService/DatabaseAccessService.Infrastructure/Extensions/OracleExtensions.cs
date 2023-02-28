using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DatabaseAccessService.Infrastructure.Extensions {

    public static class OracleExtensions {

        public static async Task<OracleDataReader> GetDBReader(this OracleConnection connection, string procedureName, List<OracleParameter>? parameters = null) {
            if (connection.State != ConnectionState.Open) await connection.OpenAsync().ConfigureAwait(false);

            OracleParameter cursor = new("results", OracleDbType.RefCursor, ParameterDirection.Output);

            //create the procedure command
            await using OracleCommand command = connection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;

            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
            command.Parameters.Add(cursor);

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            OracleRefCursor? refCursor = cursor.Value as OracleRefCursor ?? null;
            if (refCursor == null) throw new InvalidOperationException("Unable to obtain data from the database due to an error");
            OracleDataReader reader = refCursor.GetDataReader();

            return reader;
        }

        public static async Task<int> SafeGetInt(this DbDataReader reader, string columnName, int defaultValue = 0) {
            if (!await reader.IsDBNullAsync(columnName))
                return reader.GetInt32(columnName);
            return defaultValue;
        }

        public static async Task<float> SafeGetFloat(this DbDataReader reader, string columnName, float defaultValue = 0) {
            if (!await reader.IsDBNullAsync(columnName))
                return reader.GetFloat(columnName);
            return defaultValue;
        }

        public static async Task<string> SafeGetString(this DbDataReader reader, string columnName, string defaultValue = "") {
            if (!await reader.IsDBNullAsync(columnName))
                return reader.GetString(columnName);
            return defaultValue;
        }
    }
}