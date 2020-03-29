using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Core_HW.Helpers.repository;
using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;

namespace MVC_Core_HW.Controllers
{
    public class SuppliesController : Controller
    {

        private IIngredientRepository _ingredientRepository;

        public SuppliesController(IIngredientRepository ingredientRepo)
        {
            this._ingredientRepository = ingredientRepo;
        }

        public IActionResult Index()
        {
            SuppliesViewModel suppliesViewModel = new SuppliesViewModel();
            suppliesViewModel.Ingredients = this._ingredientRepository.ListAllIngredients();
            suppliesViewModel.IngredientViewModel = new IngredientCreateViewModel();
            return View(suppliesViewModel);
        }

        [HttpPost]
        public IActionResult Create()
        {
            IngredientCreateViewModel ingredientCreateViewModel = new IngredientCreateViewModel();
            try
            {
                ingredientCreateViewModel.Name = Request.Form["Name"].ToString();
                ingredientCreateViewModel.ExpirationDate = Request.Form["ExpirationDate"].ToString();
                if (ingredientCreateViewModel.Name == "" || ingredientCreateViewModel.ExpirationDate == "") throw new FormatException();
                ingredientCreateViewModel.Quantity = Convert.ToInt16(Request.Form["Quantity"].ToString());
            }catch(FormatException e)
            {
                return NotFound(e.Message);
            }
            this._ingredientRepository.CreateIngredient(ingredientCreateViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound($"Ingredient with id : {id} doesn't exist!");

            var supply = this._ingredientRepository.GetIngredientById(id.Value);

            if(supply == null) return NotFound($"Ingredient with id : {id} doesn't exist!");

            return View(supply);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound($"Ingredient with id : {id} doesn't exist!");

            var supply = this._ingredientRepository.GetIngredientById(id.Value);

            if (supply == null) return NotFound($"Ingredient with id : {id} doesn't exist!");

            return View(supply);
        }

        [HttpPost]
        public IActionResult Edit(Ingredient ingredient)
        {
            if (ingredient == null) return NotFound($"Cannot be updated, doesn't exist!");
            this._ingredientRepository.UpdateIngredient(ingredient);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Ingredient ingredient)
        {
            if (ingredient == null) return NotFound($"Cannot be deleted, doesn't exist!");
            if(this._ingredientRepository.DeleteIngredient(ingredient)) return RedirectToAction("Index");

            return NotFound($"Cannot be deleted, doesn't exist!");
        }


        [HttpGet]
        [Route("Supplies/Expired")]
        public IActionResult ExpiredProducts()
        {
            var ingredients = this._ingredientRepository.ExpiredProducts();
            return View("ListIngredients", ingredients);
        }

        [HttpGet]
        [Route("Supplies/Need")]
        public IActionResult NeededProducts()
        {
            var ingredients = this._ingredientRepository.NeededProducts();
            return View("ListIngredients", ingredients);
        }
    }
}