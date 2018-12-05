using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("recipe", Schema = "meals")]
    public class Recipe
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("servings")]
        public int Servings { get; set; }

        [Column("servingsours")]
        public int? ServingsOurs { get; set; }
    }
}
