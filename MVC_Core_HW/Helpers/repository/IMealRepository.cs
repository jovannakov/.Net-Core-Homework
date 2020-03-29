using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers
{
    public interface IMealRepository
    {
        List<ListMealsViewModel> ListAllMeals();

        List<Meal> GetMealsByType(string type);

        List<MealType> GetAllMealTypes();


        Meal GetMealByName(string name);
        Meal GetMealById(int id);

        void CreateMeal(Meal meal);

        bool DeleteMeal(Meal meal);
    }
}
