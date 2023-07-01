using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class RecipeRef
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = new Recipe().Name;
    }
}