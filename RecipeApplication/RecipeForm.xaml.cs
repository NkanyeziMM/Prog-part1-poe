using System.Collections.Generic;
using System.Windows;

namespace RecipeApplication
{
    public partial class RecipeForm : Window
    {
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<RecipeStep> steps = new List<RecipeStep>();

        public RecipeForm()
        {
            InitializeComponent();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var ingredientWindow = new IngredientWindow();
            if (ingredientWindow.ShowDialog() == true)
            {
                ingredients.Add(ingredientWindow.Ingredient);
                IngredientsListBox.Items.Add(ingredientWindow.Ingredient.GetFormattedIngredient());
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            var stepWindow = new RecipeStepWindow();
            if (stepWindow.ShowDialog() == true)
            {
                steps.Add(stepWindow.RecipeStep);
                StepsListBox.Items.Add(stepWindow.RecipeStep.Description);
            }
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe(RecipeNameTextBox.Text, ingredients, steps);
            RecipeRepository.AddRecipe(recipe);

            MessageBox.Show("Recipe saved successfully!");
            this.Close();
        }
    }
}
