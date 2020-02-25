using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Models
{
    public class RestaurantsIndexViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string SearchTerm { get; set; }
        public string TempMessage { get; set; }
        public string Message { get; set; }
    }
}
