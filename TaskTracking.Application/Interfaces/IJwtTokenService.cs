using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracking.Domain.Entities;

namespace TaskTracking.Application.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
