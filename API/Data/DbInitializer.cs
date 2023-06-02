using API.Entities;

namespace API.Data
{
   public static class DbInitializer
    {
       public static void Initialize(RecipesContext context)
       {
            if (context.Recipes.Any()) return;
            
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Jollof Rice",
                    Ingredients = "Pepper, Rice, Maggi",
                    Description = "A very delicious meal",
                    Origin = "Yoruba"
                },
                new Recipe
                {
                    Name = "Fried Rice",
                    Ingredients = "Pepper, Rice, Maggi, Curry",
                    Description = "A meal with delicate veggies",
                    Origin = "Yoruba"
                },
                new Recipe
                {
                    Name = "Efo Riro",
                    Ingredients = "Pepper, Spinnach, Maggi, Palm Oil",
                    Description = "Goes well with swallows",
                    Origin = "Yoruba"
                }
            };

            context.Recipes.AddRange(recipes);
            context.SaveChanges();
       }
    }
}