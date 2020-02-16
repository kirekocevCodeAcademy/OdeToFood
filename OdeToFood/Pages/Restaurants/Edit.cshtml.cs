using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> CuisineTypes { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
                if (Restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }
            }
            else
            {
                Restaurant = new Restaurant();
            }

            CuisineTypes = htmlHelper.GetEnumSelectList<CuisineType>(); 
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Restaurant.Id == 0)
                {
                    Restaurant = restaurantData.Create(Restaurant);
                    TempData["Message"] = "The Object is created!";
                }
                else
                {
                    Restaurant = restaurantData.Update(Restaurant);
                    TempData["Message"] = "The Object is updated!";
                }

                restaurantData.Commit();               
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            }            

            CuisineTypes = htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }
    }
}