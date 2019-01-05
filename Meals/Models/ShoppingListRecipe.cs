using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("shoppinglistrecipe", Schema = "meals")]
    public class ShoppingListRecipe
    {
        [Key, Column("shoppinglistid", Order = 0)]
        [ForeignKey("ShoppingList")]
        public int ShoppingListId { get; set; }

        [Key, Column("recipeid", Order = 1)]
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public ShoppingList ShoppingList { get; set; }

        public Recipe Recipe { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
