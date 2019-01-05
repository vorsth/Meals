using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("shoppinglistrecipe", Schema = "meals")]
    public class ShoppingListRecipe
    {
        [Key, Column("shoppinglistid", Order = 0)]
        public int ShoppingListId { get; set; }

        [Key, Column("recipeid", Order = 1)]
        public int RecipeId { get; set; }
    }
}
