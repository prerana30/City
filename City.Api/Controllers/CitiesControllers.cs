using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}", Name = "GetCity")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }

        [HttpPost]
        public ActionResult<CityDto> CreateCity([FromBody] CityDto city)
        {
            // Generate a unique ID for the new city
            var maxId = CitiesDataStore.Current.Cities.Max(c => c.Id);
            city.Id = maxId + 1;

            // Add the new city to the data store
            CitiesDataStore.Current.Cities.Add(city);

            // Return the newly created city
            return CreatedAtRoute("GetCity", new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCity(int id, [FromBody] CityDto city)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound();
            }

            // Update the existing city with the data from the request
            existingCity.Name = city.Name;
            existingCity.Description = city.Description;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateCity(int id, [FromBody] JsonPatchDocument<CityDto> patchDocument)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound();
            }

            // Apply the patch operations to the existing city
            patchDocument.ApplyTo(existingCity, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCity(int id)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound();
            }

            // Remove the existing city from the data store
            CitiesDataStore.Current.Cities.Remove(existingCity);

            return NoContent();
        }
    }
}
