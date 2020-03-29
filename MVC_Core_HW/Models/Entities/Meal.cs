using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<int> Ingredients { get; set; }

        public string Type { get; set; }

        public  bool IsVeggie { get; set; }


    }
}
