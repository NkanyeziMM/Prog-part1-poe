using System.Windows;

namespace RecipeApplication
{
    public partial class FilterRecipesWindow : Window
    {
        public FilterRecipesWindow()
        {
            InitializeComponent();
        }

        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = IngredientTextBox.Text.ToLower();
            string foodGroup = FoodGroupTextBox.Text.ToLower();
            string maxCaloriesText = MaxCaloriesTextBox.Text;

            int? maxCalories = int.TryParse(maxCaloriesText, out int parsedCalories) ? parsedCalories : (int?)null;

            var filteredRecipes = RecipeRepository.FilterRecipes(ingredient, foodGroup, maxCalories);

            FilteredRecipesListView.ItemsSource = filteredRecipes;
        }
    }
}
