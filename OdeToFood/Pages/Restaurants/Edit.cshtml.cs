using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using static OdeToFood.Core.Restaurant;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }


        /// <summary>
        /// Den här funktionen kontrollerar att det finns en resturang som det resturangId som behandlas,
        /// Om den restaurangen inte finns, så skickas användaren istället till sidan NotFound. 
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public IActionResult OnGet(int restaurantId)
        {


            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        /// <summary>
        /// Den här metoden kontrollerar om den information som 
        /// användaren fyller i på Edit sidan är korrekt. 
        /// Om den är det så redirectar den till den specifika resturangens detalj-sidan. 
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                restaurantData.Update(Restaurant);
                restaurantData.Commit();
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            }

            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();
        }



    }
}