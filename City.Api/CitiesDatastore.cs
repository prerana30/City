using City.Api.Models;

namespace City.Api
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        // singelton pattern


        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
        {
            new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "One with big park."

                },
            new CityDto()
            {
                Id = 2,
                Name = "New York",
                Description = "One with small park."

            }

        };
        
        }
    } }
