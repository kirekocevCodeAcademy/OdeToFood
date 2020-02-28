using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    public class RestaurantDto
    {
        [Required, StringLength(10)]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Cuisine { get; set; }
    }
}
