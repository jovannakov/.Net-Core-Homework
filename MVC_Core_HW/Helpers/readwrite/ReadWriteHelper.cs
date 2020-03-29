using MVC_Core_HW.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Helpers
{
    public class ReadWriteHelper : IReadWriteHelper
    {
        private string _mealsUri = @".\DataHolder\Meals.json";
        private string _ingredientsUri = @".\DataHolder\Ingredients.json";
        private string _typeUri = @".\DataHolder\MealTypes.json";

        public List<Meal> DeserializeMeals()
        {
            List<Meal> meals = new List<Meal>();
            using (StreamReader sr = new StreamReader(_mealsUri))
            {
                var json = sr.ReadToEnd();
                meals = JsonConvert
                    .DeserializeObject<List<Meal>>(json);
            }
            return meals;
        }

        public void SerializeMeals(List<Meal> meals)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(_mealsUri))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, meals);
                }
            }
        }

        public List<Ingredient> DeserializeIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (StreamReader sr = new StreamReader(_ingredientsUri))
            {
                var json = sr.ReadToEnd();
                ingredients = JsonConvert
                    .DeserializeObject<List<Ingredient>>(json);
            }
            return ingredients;
        }

        public void SerializeIngredients(List<Ingredient> ingredients)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(_ingredientsUri))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, ingredients);
                }
            }
        }

        public List<MealType> DeserializeTypes()
        {
            List<MealType> ingredients = new List<MealType>();
            using (StreamReader sr = new StreamReader(_typeUri))
            {
                var json = sr.ReadToEnd();
                ingredients = JsonConvert
                    .DeserializeObject<List<MealType>>(json);
            }
            return ingredients;
        }

        public void SerializeTypes(List<MealType> types)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(_typeUri))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, types);
                }
            }
        }
    }
}
