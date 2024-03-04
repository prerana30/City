using City.Api.Models;
using System.Collections.Generic;

namespace Address.API.Models
{
    public class AddressesDataStore
    {
        public List<AddressDto> Addresses { get; set; }
        public static AddressesDataStore Current { get; } = new AddressesDataStore();

        public AddressesDataStore()
        {
            // init dummy data for addresses
            Addresses = new List<AddressDto>()
            {
                new AddressDto()
                {
                     Id = 1,
                     Name = "Central Park",
                     Description = "The most visited urban park in the United States.",
                     Street = "123 Main St",
                     City = "Anytown",
                     Country = "USA"
                },
                new AddressDto()
                {
                    Id = 2,
                    Name = "Central Park",
                    Description = "The most visited urban park in the United States.",
                    Street = "456 Elm St",
                    City = "Othertown",
                    Country = "USA"
                },
                
            };
        }
    }
}
