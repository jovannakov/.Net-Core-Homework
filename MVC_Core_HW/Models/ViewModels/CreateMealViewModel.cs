using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Core_HW.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models.ViewModels
{
    public class CreateMealViewModel
    {
     
        [Required(ErrorMessage = "Name field is Required")]
        [StringLength(150, ErrorMessage = "Maximum length of the name is 150 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price field is Required")]
        [Range(100, 1500)]
        public int Price { get; set; }

        [Required(ErrorMessage = "Choose ingredients")]
        public List<string> Ingredients { get; set; }

        [Required]
        public string Type { get; set; }

        [Display(Name = "Is Vegetarian")]
        public bool IsVeggie { get; set; }


        public List<SelectListItem> allIngredients { get; set; }
        public List<MealType> mealTypes { get; set; }


        public CreateMealViewModel() { }
        public CreateMealViewModel(List<SelectListItem> allIngredients, List<MealType> mealTypes)
        {
            this.allIngredients = allIngredients;
            this.mealTypes = mealTypes;
            this.Ingredients = new List<string>();
        }
    }
}
