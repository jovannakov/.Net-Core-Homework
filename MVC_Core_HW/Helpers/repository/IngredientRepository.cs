using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers.repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private IReadWriteHelper _rwHelper;

        public IngredientRepository(IReadWriteHelper rwHelper)
        {
            this._rwHelper = rwHelper;
        }

        public void CreateIngredient(IngredientCreateViewModel ingredientCreateViewModel)
        {
            var ingredients = this.ListAllIngredients();
            var index = ingredients.Max(x => x.Id);
            var newIngredient = new Ingredient();
            newIngredient.Id = index + 1;
            newIngredient.Name = ingredientCreateViewModel.Name;
            newIngredient.ExpirationDate = ingredientCreateViewModel.ExpirationDate;
            newIngredient.Quantity = ingredientCreateViewModel.Quantity;

            ingredients.Add(newIngredient);
            this._rwHelper.SerializeIngredients(ingredients);
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            var allIngredients = this._rwHelper.DeserializeIngredients() as List<Ingredient>;
            var c = allIngredients.Count;
            allIngredients = allIngredients.Where(x => x.Id != ingredient.Id).ToList();

            if (allIngredients.Count == c) return false;

            this._rwHelper.SerializeIngredients(allIngredients);
            return true;
        }

        public List<Ingredient> ExpiredProducts()
        {
            var ingredients = this._rwHelper.DeserializeIngredients();
            ingredients = ingredients.Where((x) =>
            {
                if (DateTime.Parse(x.ExpirationDate) <= DateTime.Today.Date) return true;
                return false;
            }).ToList();
            return ingredients;
        }

        public Ingredient GetIngredientById(int id)
        {
            List<Ingredient> all = this.ListAllIngredients();
            return all.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public List<Ingredient> ListAllIngredients()
        {
            var ingredients = this._rwHelper.DeserializeIngredients();
            ingredients = ingredients.Where((x) =>
            {
                if (x.Quantity == 0 || DateTime.Parse(x.ExpirationDate) <= DateTime.Today.Date) return false;
                return true;
            }).ToList();
            return ingredients;
        }

        public List<Ingredient> NeededProducts()
        {
            var ingredients = this._rwHelper.DeserializeIngredients();
            ingredients = ingredients.Where((x) =>
            {
                if (x.Quantity == 0) return true;
                return false;
            }).ToList();
            return ingredients;
        }

        public void UpdateIngredient(Ingredient newIngredient)
        {
            var ingredients = this._rwHelper.DeserializeIngredients();
            ingredients.ForEach(ingredient =>
            {
                if(ingredient.Id == newIngredient.Id)
                {
                    ingredient.Name = newIngredient.Name;
                    ingredient.Quantity = newIngredient.Quantity;
                    ingredient.ExpirationDate = newIngredient.ExpirationDate;
                }
            });
            

            this._rwHelper.SerializeIngredients(ingredients);
        }
    }
}
