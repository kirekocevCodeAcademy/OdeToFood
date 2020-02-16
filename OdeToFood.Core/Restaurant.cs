using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required, StringLength(10)]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        [Required]
        public CuisineType? Cuisine { get; set; }
    }
}
