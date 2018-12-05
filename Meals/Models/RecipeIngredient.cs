using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("recipeingredient", Schema = "meals")]
    public class RecipeIngredient
    {
        [Key, Column("recipeid", Order = 0)]
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [Key, Column("ingredientid", Order = 1)]
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        public Recipe Recipe { get; set; }

        public Ingredient Ingredient { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unitid")]
        public int UnitId { get; set; }
    }
}
