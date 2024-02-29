using City.Api;
using Microsoft.AspNetCore.Mvc;


namespace City.Controller
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesControllers : ControllerBase

    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
                 
        }
    }
}
