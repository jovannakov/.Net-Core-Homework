using MVC_Core_HW.Helpers.repository;
using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers
{
    public class MealRepository : IMealRepository
    {
        
        private IReadWriteHelper _rwHelper;
        private IIngredientRepository _ingredientRepository;
        public MealRepository(IReadWriteHelper rwHelper, IIngredientRepository ingredientRepository) {
            this._rwHelper = rwHelper;
            this._ingredientRepository = ingredientRepository;
        }

        public void CreateMeal(Meal meal)
        {
            List<Meal> meals = this._rwHelper.DeserializeMeals();
            int index = meals.Max(x => x.Id);
            meal.Id = index + 1;
            meals.Add(meal);
            this._rwHelper.SerializeMeals(meals);
        }

        

        public List<MealType> GetAllMealTypes()
        {
            var mealTypes = this._rwHelper.DeserializeTypes();
            return mealTypes;
        }

        public List<Meal> GetMealsByType(string type)
        {
            var meals = this._rwHelper.DeserializeMeals();
            var returnMeal = meals
                .Where(x => x.Type.ToLower().Equals(type))
                .ToList();

            return returnMeal as List<Meal>;
        }

        public List<ListMealsViewModel> ListAllMeals()
        {
            List<Meal> meals = this._rwHelper.DeserializeMeals();

            List<ListMealsViewModel> listMealsViewModel = meals.Select((x) =>
            {
                ListMealsViewModel mealsViewModel = new ListMealsViewModel();
                mealsViewModel.Id = x.Id;
                mealsViewModel.Name = x.Name;
                mealsViewModel.Price = x.Price;
                mealsViewModel.Type = x.Type;
                foreach(var id in x.Ingredients)
                {
                    mealsViewModel.Ingredients.Add(this._ingredientRepository.GetIngredientById(id));
                }
                mealsViewModel.IsVeggie = x.IsVeggie;
                return mealsViewModel;
            })
                .ToList();
            return listMealsViewModel;
        }


        public List<Ingredient> ListIngredientsPerMeal(Meal meal)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(var id in meal.Ingredients)
            {
                ingredients.Add(this._ingredientRepository.GetIngredientById(id));
            }
            return ingredients;
        }

       
        public Meal GetMealByName(string name)
        {
            List<Meal> meals = this._rwHelper.DeserializeMeals();
            Meal m = meals
                .Where(x => x.Name.Equals(name))
                .First();
            return m;
        }

        public bool DeleteMeal(Meal meal)
        {
            List<Meal> meals = this._rwHelper.DeserializeMeals();
            int numberOfMeals = meals.Count;
            meals = meals.Where(x => x.Id != meal.Id).ToList(); 
            if (numberOfMeals == meals.Count) return false;
            this._rwHelper.SerializeMeals(meals);
            return true;
        }

        public Meal GetMealById(int id)
        {
            List<Meal> meals = this._rwHelper.DeserializeMeals();

            var meal = meals.Where(x => x.Id == id).First() as Meal;
            return meal;
        }
    }
}
