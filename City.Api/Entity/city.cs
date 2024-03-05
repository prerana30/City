namespace City.Api.Entity
{
    public class city
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
                                           = new List<PointOfInterest>(); // initialize this to a empty list to avoid null reference.
    }
}
