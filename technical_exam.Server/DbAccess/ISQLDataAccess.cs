using System.Collections.Generic;
using System.Threading.Tasks;

namespace technical_exam.Server.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "TechnicalExamDb");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "TechnicalExamDb");
    }
}
