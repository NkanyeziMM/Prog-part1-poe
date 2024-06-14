using System.Windows;

namespace RecipeApplication
{
    public partial class RecipeStepWindow : Window
    {
        public RecipeStep RecipeStep { get; private set; }

        public RecipeStepWindow()
        {
            InitializeComponent();
            RecipeStep = new RecipeStep(); // Initialize with a default value
        }

        private void SaveStep_Click(object sender, RoutedEventArgs e)
        {
            RecipeStep.Description = DescriptionTextBox.Text;

            DialogResult = true;
            this.Close();
        }
    }
}
