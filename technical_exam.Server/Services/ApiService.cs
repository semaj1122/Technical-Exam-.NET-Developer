using technical_exam.Server.Interfaces;
using Microsoft.Extensions.Configuration;
using technical_exam.Server.Models;

namespace technical_exam.Server.Services
{
    public class ApiService : IApiServiceRepository
    {
        public readonly string username = null;
        public readonly string password = null;
        public ApiService(IConfiguration configuration)
        {
            username = configuration.GetValue<string>("SwaggerBasicAuthentication:Username");
            password = configuration.GetValue<string>("SwaggerBasicAuthentication:Password");
        }
        public ApiCredentials Authenticate(string username, string password)
        {
            var apiUser = new ApiCredentials
            {
                Username = this.username,
                Password = this.password
            };

            if (apiUser.Password.ToString() != password.ToString() || apiUser.Username.ToString() != username.ToString())
            {
                return null;
            }
            else
            {
                return apiUser;
            }
        }
    }
}
