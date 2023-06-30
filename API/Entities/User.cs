using API.HelperFunctions;

namespace API.Entities
{
   public class User
    {       
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RecipeHelper<Recipe> Bookmarks { get; set; }
        public List<Recipe> Favorites { get; set; }
        public List<Recipe> RatedRecipes { get; set; }
        public List<Recipe> RatedRestaurant { get; set; }

    }
}

//Users: name, password, email, bookmarks, favorites, ratedRecioe, RatedRestaurant