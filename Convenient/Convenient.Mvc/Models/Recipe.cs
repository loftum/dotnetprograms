using System;
using System.Collections.Generic;

namespace Convenient.Mvc.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset UpdatedTime { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Dictionary<string, bool> Properties { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            CreatedTime = DateTime.Now;
            UpdatedTime = CreatedTime;
            Properties = new Dictionary<string, bool>();
        }
    }
}