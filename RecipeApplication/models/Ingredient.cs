namespace RecipeApplication
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double? Calories { get; set; }
        public string FoodGroup { get; set; }

        public void ScaleQuantity(double factor)
        {
            Quantity *= factor;
        }

        public string GetFormattedIngredient()
        {
            return $"{Quantity} {Unit} of {Name}";
        }
    }
}
