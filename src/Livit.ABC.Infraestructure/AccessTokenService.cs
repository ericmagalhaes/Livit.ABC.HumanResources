using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Livit.ABC.Infraestructure
{
    public class AccessTokenService
    {
        private readonly IHttpContextAccessor _accessor;

        public AccessTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public string GetValue()
        {
            return _accessor.HttpContext.Session.GetString("access_token");
        }
    }
}
