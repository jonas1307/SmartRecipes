using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartRecipies.Backend.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Servings { get; set; }

        public int Calories { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public string Instructions { get; set; }
    }
}
