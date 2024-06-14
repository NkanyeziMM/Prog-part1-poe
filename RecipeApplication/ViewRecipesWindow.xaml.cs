using System.Windows;

namespace RecipeApplication
{
    public partial class ViewRecipesWindow : Window
    {
        public ViewRecipesWindow()
        {
            InitializeComponent();
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
