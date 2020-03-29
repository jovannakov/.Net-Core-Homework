using MVC_Core_HW.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers
{
    public interface IReadWriteHelper
    {
        List<Meal> DeserializeMeals();

        void SerializeMeals(List<Meal> meals);
        List<Ingredient> DeserializeIngredients();

        void SerializeIngredients(List<Ingredient> ingredients);
        List<MealType> DeserializeTypes();

        void SerializeTypes(List<MealType> types);
    }
}
