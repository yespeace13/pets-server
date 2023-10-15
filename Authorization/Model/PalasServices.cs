using System.Security.Claims;

namespace PetsServer.Authorization.Model
{
    public class PalasServices : IPalasServices
    {
        private readonly IHttpContextAccessor _http;
        public PalasServices(IHttpContextAccessor http)
        {
            _http = http;
        }
        public string GetCurrentUser()
        {
            return _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
}
