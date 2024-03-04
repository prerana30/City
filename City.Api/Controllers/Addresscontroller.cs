using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Address.API.Models;
using City.Api.Models;

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
    }
}
