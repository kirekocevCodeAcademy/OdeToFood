using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Models
{
    public class RestaurantEditViewModel
    {
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisineTypes { get; set; }
    }
}
