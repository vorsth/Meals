using System.Collections.Generic;
using Meals.Models;

namespace Meals.ViewModels
{
    public class ShopVM
    {
        public ShoppingList ShoppingList { get; set; }
        public IEnumerable<ShoppingListIngredient> Ingredients { get; set; }

        public ShopVM(ShoppingList list, IEnumerable<ShoppingListIngredient> ingredients)
        {
            this.ShoppingList = list;
            this.Ingredients = ingredients;
        }
    }
}
