using System.Collections.Generic;

namespace RecipeApplication
{
    public class Recipe
    {
        public string Name { get; set; } // Name of the recipe
        public List<Ingredient> Ingredients { get; set; } // List to store ingredients
        public List<RecipeStep> Steps { get; set; } // List to store recipe steps

        // Constructor to initialize a recipe with specified name, ingredients, and steps
        public Recipe(string name, List<Ingredient> ingredients, List<RecipeStep> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        // Method to scale all ingredients in the recipe
        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ScaleQuantity(factor);
            }
        }

        // Method to reset all ingredient quantities to their original values
        public void ResetRecipe()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = 0; // Reset quantity to zero
            }
        }

        // Method to calculate the total calories of the recipe
        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.Calories.HasValue)
                {
                    totalCalories += ingredient.Calories.Value * ingredient.Quantity;
                }
            }
            return totalCalories;
        }
    }
}
