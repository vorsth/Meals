using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("mealrecipe", Schema = "meals")]
    public class MealRecipe
    {
        [Key, Column("mealid", Order = 0)]
        public int MealId { get; set; }

        [Key, Column("recipeid", Order = 1)]
        public int RecipeId { get; set; }
    }
}
