using MVC_Core_HW.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models.ViewModels
{
    public class SuppliesViewModel
    {
        public List<Ingredient> Ingredients { get; set; }
        public IngredientCreateViewModel IngredientViewModel { get; set; }
    }
}
