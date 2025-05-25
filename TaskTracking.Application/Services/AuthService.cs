using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracking.Application.Interfaces;

namespace TaskTracking.Application.Services
{
    internal class AuthService : IAuthService
    {
        public Task<string> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
