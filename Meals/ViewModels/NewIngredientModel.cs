using System.Collections.Generic;
using Meals.Models;

namespace Meals.ViewModels
{
    public class NewIngredientModel
    {
        public IEnumerable<Store> Stores { get; set; }
    }
}
