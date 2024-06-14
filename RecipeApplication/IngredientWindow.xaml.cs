using System.Windows;

namespace RecipeApplication
{
    public partial class IngredientWindow : Window
    {
        public Ingredient Ingredient { get; private set; }

        public IngredientWindow()
        {
            InitializeComponent();
            Ingredient = new Ingredient(); // Initialize with a default value
        }

        private void SaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            double quantity = double.Parse(QuantityTextBox.Text);
            string unit = UnitTextBox.Text;
            double? calories = string.IsNullOrEmpty(CaloriesTextBox.Text) ? (double?)null : double.Parse(CaloriesTextBox.Text);
            string foodGroup = FoodGroupTextBox.Text;

            Ingredient.Name = name;
            Ingredient.Quantity = quantity;
            Ingredient.Unit = unit;
            Ingredient.Calories = calories;
            Ingredient.FoodGroup = foodGroup;

            DialogResult = true;
            this.Close();
        }
    }
}
