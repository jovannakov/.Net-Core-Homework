using MVC_Core_HW.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models.ViewModels
{
    public class ListMealsViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public string Type { get; set; }

        public bool IsVeggie { get; set; }

        public ListMealsViewModel()
        {
            this.Ingredients = new List<Ingredient>();
        }
    }
}
