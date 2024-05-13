using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApplication
{
    // Enum for food groups
    enum FoodGroup
    {
        Meat,
        Dairy,
        Vegetable,
        Fruit,
        Grain
    }

    // Ingredient class with additional properties: Calories and FoodGroup
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double OriginalQuantity { get; set; }
        public double Calories { get; set; }
        public FoodGroup FoodGroup { get; set; }

        public void ScaleQuantity(double factor)
        {
            Quantity *= factor;
        }

        public void ResetQuantity()
        {
            Quantity = OriginalQuantity;
        }

        public string GetFormattedIngredient()
        {
            return $"{Quantity} {Unit} of {Name}";
        }
    }

    // RecipeStep class
    class RecipeStep
    {
        public string Description { get; set; }
    }

    // Recipe class
    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipeStep> Steps { get; set; }

        // Delegate to notify when recipe exceeds 300 calories
        public delegate void RecipeCaloriesExceedsHandler(string recipeName, double totalCalories);

        // Event to trigger when recipe exceeds 300 calories
        public event RecipeCaloriesExceedsHandler OnRecipeCaloriesExceeds;

        public Recipe(string name, List<Ingredient> ingredients, List<RecipeStep> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ScaleQuantity(factor);
            }
        }

        public void ResetRecipe()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ResetQuantity();
            }
        }

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories * i.Quantity);
        }

        public void CheckCalories()
        {
            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300 && OnRecipeCaloriesExceeds != null)
            {
                OnRecipeCaloriesExceeds(Name, totalCalories);
            }
        }
    }

    class Program
    {
        static List<Recipe> savedRecipes = new List<Recipe>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nWelcome To The Recipe Application!");
                Console.WriteLine("1. Add a New Recipe");
                Console.WriteLine("2. View Saved Recipes");
                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewRecipe();
                        break;
                    case "2":
                        ViewSavedRecipes();
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddNewRecipe()
        {
            Console.WriteLine("\nEnter recipe details:");

            Console.Write("Add Name of the recipe: ");
            string recipeName = Console.ReadLine();

            int numIngredients = GetIntegerInput("Number of ingredients: ");
            List<Ingredient> ingredients = new List<Ingredient>();

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"\nEnter details for ingredient {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();

                double quantity;
                do
                {
                    quantity = GetDoubleInput("Quantity: ");
                    if (quantity <= 0)
                    {
                        Console.WriteLine("Quantity must be greater than 0. Please enter a valid quantity.");
                    }
                } while (quantity <= 0);

                Console.Write("Unit: ");
                string unit = Console.ReadLine();

                double calories = GetDoubleInput("Calories: ");

                FoodGroup foodGroup;
                do
                {
                    Console.Write("Food Group (Meat, Dairy, Vegetable, Fruit, Grain): ");
                } while (!Enum.TryParse(Console.ReadLine(), true, out foodGroup));

                ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, OriginalQuantity = quantity, Calories = calories, FoodGroup = foodGroup });
            }

            int numSteps = GetIntegerInput("Number of steps: ");
            List<RecipeStep> steps = new List<RecipeStep>();

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"\nEnter details for step {i + 1}:");
                Console.Write("Description: ");
                string description = Console.ReadLine();

                steps.Add(new RecipeStep { Description = description });
            }

            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            recipe.OnRecipeCaloriesExceeds += RecipeCaloriesExceedsHandler;
            savedRecipes.Add(recipe);

            recipe.CheckCalories(); // Check if total calories exceed 300

            Console.WriteLine("\nRecipe added successfully!");
        }

        static void ViewSavedRecipes()
        {
            if (savedRecipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes saved yet.");
                return;
            }

            Console.WriteLine("\nSaved Recipes:");
            var sortedRecipes = savedRecipes.OrderBy(r => r.Name);
            int recipeIndex = 1;

            foreach (var recipe in sortedRecipes)
            {
                Console.WriteLine($"{recipeIndex}. {recipe.Name}");
                recipeIndex++;
            }

            int selectedRecipeIndex = GetIntegerInput("\nEnter the number of the recipe you want to view or scale: ") - 1;

            if (selectedRecipeIndex < 0 || selectedRecipeIndex >= savedRecipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            var selectedRecipe = sortedRecipes.ElementAt(selectedRecipeIndex);
            DisplayRecipe(selectedRecipe);

            Console.WriteLine("\nTotal Calories: " + selectedRecipe.CalculateTotalCalories());

            selectedRecipe.CheckCalories(); // Check if total calories exceed 300

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Scale Recipe");
            Console.WriteLine("2. Reset Recipe");
            Console.WriteLine("3. Return to Main Menu");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ScaleRecipe(selectedRecipe);
                    break;
                case "2":
                    ResetRecipe(selectedRecipe);
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine($"Recipe Name: {recipe.Name}");

            Console.WriteLine("\nIngredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.GetFormattedIngredient()}, Calories: {ingredient.Calories}, Food Group: {ingredient.FoodGroup}");
            }

            Console.WriteLine("\nSteps:");
            int stepIndex = 1;
            foreach (var step in recipe.Steps)
            {
                Console.WriteLine($"Step {stepIndex}: {step.Description}");
                stepIndex++;
            }
        }

        static void ScaleRecipe(Recipe recipe)
        {
            double factor;
            do
            {
                factor = GetDoubleInput("Enter scaling factor (0.5, 2, 3): ");
                if (factor <= 0)
                {
                    Console.WriteLine("Scaling factor must be greater than 0. Please enter a valid scaling factor.");
                }
            } while (factor <= 0);

            recipe.ScaleRecipe(factor);

            Console.WriteLine("\nRecipe scaled successfully:");
            DisplayRecipe(recipe);
        }

        static void ResetRecipe(Recipe recipe)
        {
            recipe.ResetRecipe();
            Console.WriteLine("\nRecipe quantities reset to original values:");
            DisplayRecipe(recipe);
        }

        static int GetIntegerInput(string message)
        {
            int result;
            bool success;
            do
            {
                Console.Write(message);
                success = int.TryParse(Console.ReadLine(), out result);
                if (!success)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            } while (!success);
            return result;
        }

        static double GetDoubleInput(string message)
        {
            double result;
            bool success;
            do
            {
                Console.Write(message);
                success = double.TryParse(Console.ReadLine(), out result);
                if (!success)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            } while (!success);
            return result;
        }

        static void RecipeCaloriesExceedsHandler(string recipeName, double totalCalories)
        {
            Console.WriteLine($"\nWarning: Total calories for '{recipeName}' exceed 300! Total Calories: {totalCalories}");
        }
    }
}
