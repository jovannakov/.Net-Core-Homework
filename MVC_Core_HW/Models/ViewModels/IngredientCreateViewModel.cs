using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models.ViewModels
{
    public class IngredientCreateViewModel
    {
        public string Name { get; set; }

        public string ExpirationDate { get; set; }

        public int Quantity { get; set; }
    }
}
