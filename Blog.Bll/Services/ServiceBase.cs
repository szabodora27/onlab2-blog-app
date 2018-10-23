using Blog.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Bll.Services
{
    public class ServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _currentUserGuid;

        public ServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUserGuid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        protected string GetCurrentUserId()
        {
            return _currentUserGuid;
        }
    }
}
