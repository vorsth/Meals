using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meals.Models
{
    [Table("shoppinglist", Schema = "meals")]
    public class ShoppingList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("creationdate")]
        public DateTime CreationDate { get; set; }
    }
}
