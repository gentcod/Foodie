using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
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