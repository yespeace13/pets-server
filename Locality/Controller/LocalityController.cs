using Microsoft.AspNetCore.Mvc;
using PetsServer.Locality.Model;
using PetsServer.Locality.Repository;

namespace PetsServer.Locality.Controller
{
    [ApiController]
    [Route("localitys")]
    public class LocalityController : ControllerBase
    {
        [HttpGet(Name = "GetLocalitys")]
        public ActionResult<List<LocalityModel>> Index()
        {
            return new ActionResult<List<LocalityModel>>(new LocalityRepository().GetLocalitys());
        }
        [HttpGet("{id}", Name = "GetLocality")]
        public ActionResult<LocalityModel> Details(int id)
        {
            return new ActionResult<LocalityModel>(new LocalityRepository().GetOne(id));
        }
    }
}
