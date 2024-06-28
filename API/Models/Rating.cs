namespace API.Models;
public class Rating
{
   public int Id { get; set; }
   
   public Guid UserId { get; set; }
   public User User { get; set; }

   public required int RatingNum { get; set; }
   public required string Comment { get; set; }
}
