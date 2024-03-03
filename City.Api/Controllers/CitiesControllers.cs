using City.Api;
using Microsoft.AspNetCore.Mvc;


using City.Api.Models;


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

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn= CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id);


            if(cityToReturn==null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);

                
        }
    }
}
