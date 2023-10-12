using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportLibrary.Model.Locality;

namespace PetsServer.Locality
{
    [ApiController]
    [Route("localitys")]
    public class LocalityController : Controller
    {
        [HttpGet(Name ="GetLocalitys")]
        public ActionResult<List<LocalityModel>> Index()
        {
            return new ActionResult<List<LocalityModel>>(new LocalityRepository().GetLocalitys());
        }
        [HttpGet("{id}", Name ="GetLocality")]
        public ActionResult<LocalityModel> Details(int id)
        {
            return new ActionResult<LocalityModel>(new LocalityRepository().GetOne(id));
        }
    }
}
