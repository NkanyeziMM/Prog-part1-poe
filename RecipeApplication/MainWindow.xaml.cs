using System.Windows;

namespace RecipeApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow();
            addRecipeWindow.ShowDialog();
        }

        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            var viewRecipesWindow = new ViewRecipesWindow();
            viewRecipesWindow.ShowDialog();
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            var filterRecipesWindow = new FilterRecipesWindow();
            filterRecipesWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
