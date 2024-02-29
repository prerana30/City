namespace City.Api.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } // ? to show it's non-nullable
        // Memory Data Store and DTO classes
    }
}
