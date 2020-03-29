namespace MVC_Core_HW.Models.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ExpirationDate { get; set; }

        public int Quantity { get; set; }
    }
}