using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace technical_exam.Server.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;
        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "TechnicalExamDb")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "TechnicalExamDb")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
