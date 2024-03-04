using City.Api.Models;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest/{pointOfInterestId}/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> GetAddresses(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest.Addresses);
        }

        [HttpGet("{addressId}", Name = "GetAddress")]
        public ActionResult<AddressDto> GetAddress(int cityId, int pointOfInterestId, int addressId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var address = pointOfInterest.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost]
        public ActionResult<AddressDto> CreateAddress(int cityId, int pointOfInterestId,
            [FromBody] AddressDto address)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            address.Id = pointOfInterest.Addresses.Max(a => a.Id) + 1;
            pointOfInterest.Addresses.Add(address);

            return CreatedAtRoute("GetAddress",
                 new
                 {
                     cityId = cityId,
                     pointOfInterestId = pointOfInterestId,
                     addressId = address.Id
                 },
                 address);
        }

        [HttpPut("{addressId}")]
        public ActionResult UpdateAddress(int cityId, int pointOfInterestId, int addressId,
    [FromBody] AddressForUpdateDto address)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var existingAddress = pointOfInterest.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (existingAddress == null)
            {
                return NotFound();
            }

            // Update address properties
            existingAddress.Street = address.Street;
            existingAddress.City = address.City;
            existingAddress.Country = address.Country;

            return NoContent();
        }


        [HttpPatch("{addressId}")]
        public ActionResult PartiallyUpdateAddress(int cityId, int pointOfInterestId, int addressId,
    [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var existingAddress = pointOfInterest.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (existingAddress == null)
            {
                return NotFound();
            }

            var addressToPatch = new PointOfInterestForUpdateDto
            {
                Name = existingAddress.Name,
                Description = existingAddress.Description
            };

            // Apply patch operations to address
            patchDocument.ApplyTo(addressToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Update existing address with patched values
            existingAddress.Name = addressToPatch.Name;
            existingAddress.Description = addressToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{addressId}")]
        public ActionResult DeleteAddress(int cityId, int pointOfInterestId, int addressId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var addressToDelete = pointOfInterest.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (addressToDelete == null)
            {
                return NotFound();
            }

            pointOfInterest.Addresses.Remove(addressToDelete);

            return NoContent();
        }
    }
}
