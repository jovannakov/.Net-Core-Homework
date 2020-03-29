using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Core_HW.Helpers;
using MVC_Core_HW.Helpers.repository;
using MVC_Core_HW.Models;
using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;

namespace MVC_Core_HW.Controllers
{
    
    public class RestaurantController : Controller
    {
        private IMealRepository _mealRepository;
        private IIngredientRepository _ingredientRepository;

        public RestaurantController(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            this._mealRepository = mealRepository;
            this._ingredientRepository = ingredientRepository;
        }


        public IActionResult Index()
        {
            List<ListMealsViewModel> meals = this._mealRepository.ListAllMeals();
            return View(meals);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound($"Select meal to delete first");

            var meal = this._mealRepository.GetMealById(id.Value);

            if (meal == null) return NotFound($"Meal with id : {id} doesn't exist!");

            return View(meal);
        }

        [HttpPost]
        public IActionResult Delete(Meal meal)
        {
            if (meal == null) return NotFound($"Cannot be deleted, doesn't exist!");
            if (this._mealRepository.DeleteMeal(meal)) return RedirectToAction("Index");

            return NotFound($"Cannot be deleted, doesn't exist!");
        }

        [Route("Restaurant/create/meal")]
        [HttpGet]
        public IActionResult CreateMeal()
        {
            return View(this.CreateTheViewModelForMeal());
        }

        private CreateMealViewModel CreateTheViewModelForMeal()
        {
            var allTypes = this._mealRepository.GetAllMealTypes();
            var allIngredients = this._ingredientRepository.ListAllIngredients().Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList();
            CreateMealViewModel createMealViewModel = new CreateMealViewModel(allIngredients, allTypes);
            return createMealViewModel;
        }

        [Route("Restaurant/create/meal")]
        [HttpPost]
        public IActionResult CreateMeal(CreateMealViewModel createMealViewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("CreateMeal");
            if (createMealViewModel.Type == "Desert" && createMealViewModel.Price < 250)
            {
                return View(this.CreateTheViewModelForMeal());
            }
            Meal meal = new Meal();
            meal.Name = createMealViewModel.Name;
            meal.Price = createMealViewModel.Price;
            meal.Type = createMealViewModel.Type;
            meal.Ingredients = createMealViewModel.Ingredients.Select(x => Convert.ToInt32(x)).ToList();
            meal.IsVeggie = createMealViewModel.IsVeggie != null ? createMealViewModel.IsVeggie : false;

            this._mealRepository.CreateMeal(meal);
            return RedirectToAction("Index");
        }

        [Route("Restaurant/{type}/meals")]
        public IActionResult MealsByType(string type)
        {
            List<Meal> meals = this._mealRepository.GetMealsByType(type.ToLower());
            Helper helper = new Helper();
            string time = DateTime.Now.ToString("HH:mm:ss tt");
            helper.Log(new SearchTypeLog(type.ToLower(), time));
            return View(meals);
        }
    }
}