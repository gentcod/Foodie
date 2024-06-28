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

        Ratings.Add(new Rating
        {
            UserId = rating.User.UserId,
            RatingNum = rating.RatingNum,
            Comment = rating.Comment,
        });

        TotalRatings = Ratings.Count;
    }

    public Rating RemoveRating(Guid userId)
    {
        var rating = Ratings.FirstOrDefault(el => el.UserId == userId);
        if (rating == null) return null;

        Ratings.Remove(rating);
        TotalRatings = Ratings.Count;
        return rating;
    }
}
