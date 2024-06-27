namespace API.Models;
public class Restaurant
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string ImageSrc { get; set; }
    public double RatingNum { get; set; }
    public Bearing Geolocation { get; set; }
    public RestaurantRatings RestaurantRatings { get; set; }
}
