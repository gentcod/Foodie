namespace API.Models.References
{
    public class FavoriteRecipe
   {
      public int Id { get; set; }

      public int FavoritesNum { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int FavoritesId { get; set; }
        public Favorites Favorites { get; set; }
   }
}