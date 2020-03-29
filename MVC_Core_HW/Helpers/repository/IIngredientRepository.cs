using MVC_Core_HW.Models.Entities;
using MVC_Core_HW.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers.repository
{
    public interface IIngredientRepository
    {
        List<Ingredient> ListAllIngredients();
        Ingredient GetIngredientById(int id);
        void CreateIngredient(IngredientCreateViewModel ingredient);
        void UpdateIngredient(Ingredient ingredient);
        bool DeleteIngredient(Ingredient ingredient);
        List<Ingredient> ExpiredProducts(); 
        List<Ingredient> NeededProducts();
    }
}
