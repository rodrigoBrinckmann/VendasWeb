using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebInfrastructure.AuthServices
{
    public interface IAuthenticatedUser
    {
        string Role { get; }
        string Email { get; }
        IEnumerable<Claim> GetClaimsIdentity();
    }

    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Email => GetClaimsIdentity().FirstOrDefault(a => a.Type == "username")?.Value; 
        //_accessor.HttpContext.User.Identity.Name;
        public string Role => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
