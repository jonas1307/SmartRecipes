using System.ComponentModel.DataAnnotations;

namespace SmartRecipies.Backend.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
