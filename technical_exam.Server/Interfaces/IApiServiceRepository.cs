using technical_exam.Server.Models;

namespace technical_exam.Server.Interfaces
{
    public interface IApiServiceRepository
    {
        ApiCredentials Authenticate(string username, string password);
    }
}
