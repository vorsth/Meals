using Meals.Models;

namespace Meals.ViewModels
{
    public class ShoppingListIngredient
    {
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }

        public ShoppingListIngredient(Ingredient i, int quantity, Unit u)
        {
            this.Ingredient = i;
            this.Quantity = quantity;
            this.Unit = u;
        }

        public override string ToString()
        {
            return $"{Quantity} {Unit.Name} {Ingredient.Name}";
        }
    }
}
