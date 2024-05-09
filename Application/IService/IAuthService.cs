using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAuthService
    {
        bool CheckCookies(HttpRequest Request);
        string GenerateToken(string username, string role);
        string GetSecretKey();

    }
}
