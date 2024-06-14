using System.Collections.Generic;
using System.Linq;

namespace RecipeApplication
{
    public static class RecipeRepository
    {
        private static List<Recipe> recipes = new List<Recipe>();

        public static void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public static List<Recipe> GetRecipes()
        {
            return recipes;
        }

        public static List<Recipe> FilterRecipes(string ingredient, string foodGroup, int? maxCalories)
        {
            var filteredRecipes = recipes.AsQueryable();

            if (!string.IsNullOrEmpty(ingredient))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredient.ToLower())));
            }

            if (!string.IsNullOrEmpty(foodGroup))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.ToLower().Contains(foodGroup.ToLower())));
            }

            if (maxCalories.HasValue)
            {
                filteredRecipes = filteredRecipes.Where(r => r.CalculateTotalCalories() <= maxCalories.Value);
            }

            return filteredRecipes.ToList();
        }
    }
}
