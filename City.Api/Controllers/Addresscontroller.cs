using Microsoft.AspNetCore.Mvc;
using Address.API.Models;
using City.Api.Models;

using CityInfo.API.Models;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Controllers;
using Microsoft.AspNetCore.JsonPatch;

namespace Address.API.Controllers

{
    [ApiController]
    [Route("api/addresses")]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> GetAddresses()
        {
            return Ok(AddressesDataStore.Current.Addresses);
        }

        [HttpGet("{id}")]
        public ActionResult<AddressDto> GetAddress(int id)
        {
            var addressToReturn = AddressesDataStore.Current.Addresses.FirstOrDefault(a => a.Id == id);

            if (addressToReturn == null)
            {
                return NotFound();
            }

            return Ok(addressToReturn);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest();
            }

            // Generate a unique ID for the new address
            addressDto.Id = AddressesDataStore.Current.Addresses.Max(a => a.Id) + 1;

            AddressesDataStore.Current.Addresses.Add(addressDto);

            // Return the newly created address
            return CreatedAtAction(nameof(GetAddress), new { id = addressDto.Id }, addressDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] AddressDto addressDto)
        {
            var addressToUpdate = AddressesDataStore.Current.Addresses.FirstOrDefault(a => a.Id == id);

            if (addressToUpdate == null)
            {
                return NotFound();
            }

            // Update all properties of the address
            addressToUpdate.Street = addressDto.Street;
            addressToUpdate.City = addressDto.City;
            addressToUpdate.Country = addressDto.Country;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateAddress(int id, [FromBody] JsonPatchDocument<AddressDto> patchDoc)
        {
            var addressToUpdate = AddressesDataStore.Current.Addresses.FirstOrDefault(a => a.Id == id);

            if (addressToUpdate == null)
            {
                return NotFound();
            }

            var addressDto = new AddressDto
            {
                Id = addressToUpdate.Id,
                Street = addressToUpdate.Street,
                City = addressToUpdate.City,
                Country = addressToUpdate.Country
            };

            patchDoc.ApplyTo(addressDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            addressToUpdate.Street = addressDto.Street;
            addressToUpdate.City = addressDto.City;
            addressToUpdate.Country = addressDto.Country;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var addressToDelete = AddressesDataStore.Current.Addresses.FirstOrDefault(a => a.Id == id);

            if (addressToDelete == null)
            {
                return NotFound();
            }

            AddressesDataStore.Current.Addresses.Remove(addressToDelete);

            return NoContent();
        }
    }
}
