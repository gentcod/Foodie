namespace API.Entities
{
   public class BookmarkItem
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int BookmarksId { get; set; }
    }
}