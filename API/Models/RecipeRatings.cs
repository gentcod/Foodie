namespace API.Models;
public class RecipeRatings
{
    public int Id { get; set;}
    public int TotalRatings { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public List<Rating> Ratings { get; set; }

    public void AddRating(Rating rating)
    {
        Ratings ??= [];

        var existingRating = Ratings.FirstOrDefault(el => el.UserId == rating.UserId);
        if (existingRating != null) return;

        Ratings.Add(new Rating
        {
            UserId = rating.UserId,
            RatingNum = rating.RatingNum,
            Comment = rating.Comment,
        });

        TotalRatings = Ratings.Count;
    }

    public void RemoveRating(string userId)
    {
        var rating = Ratings.FirstOrDefault(el => el.UserId == userId);
        if (rating == null) return;

        Ratings.Remove(rating);
        TotalRatings = Ratings.Count;
    }
}
