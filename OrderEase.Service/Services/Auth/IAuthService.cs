using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEase.Service.Services.Auth
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(string entityId, string entityType, string phone);
    }
    public class AuthService : IAuthService
    {
        public Task<string> GenerateTokenAsync(string entityId, string entityType, string phone)
        {
            
        }
    }
}
