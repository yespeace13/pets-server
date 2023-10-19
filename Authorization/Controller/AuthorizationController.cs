using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetsServer.Controler
{
    [Authorize]
    public class AuthorizationController : ControllerBase
    {

    }
}
