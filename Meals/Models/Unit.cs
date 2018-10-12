using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("unit", Schema = "meals")]
    public class Unit
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("baseunitid")]
        public int? BaseUnitId { get; set; }

        [Column("multiplier")]
        public double? Multiplier { get; set; }
    }
}
