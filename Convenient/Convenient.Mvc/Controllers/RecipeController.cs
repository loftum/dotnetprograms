using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Convenient.Models;
using Convenient.Mvc.Models;

namespace Convenient.Mvc.Controllers
{
    public class RecipeController : Controller
    {
        private static readonly IDictionary<Guid, Recipe> Recipes = new Dictionary<Guid, Recipe>();

        static RecipeController()
        {
            var polse = new Recipe {Id = Guid.Parse("2690d6dd-2e53-4559-9a9c-cdf6d581c25b"), Name = "Pølse"};
            AddOrUpdate(polse);
            AddOrUpdate(new Recipe { Id = Guid.Parse("2a5448d1-ae43-4eaa-9cd8-4369e2854358"), Name = "Milkshake" });
            AddOrUpdate(new Recipe { Id = Guid.Parse("df48ab3d-46a9-4196-89a1-11cf13889930"), Name = "Øl" });
        }

        private static void AddOrUpdate(Recipe recipe)
        {
            Recipes[recipe.Id] = recipe;
        }

        public ActionResult Edit(Guid id)
        {
            var recipe = Recipes[id];
            return View(ComplexObjectModel.For(recipe, o => o
                .For<Recipe>(c => c
                    .ReadOnly(r => r.Id)
                    .ReadOnly(r => r.CreatedTime)
                    .ReadOnly(r => r.UpdatedTime)
                    )
                ));
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe)
        {
            recipe.UpdatedTime = DateTime.Now;
            AddOrUpdate(recipe);
            return RedirectToAction("Edit", new {id = recipe.Id});
        }

        public ActionResult Index()
        {
            return View(Recipes.Select(r => r.Value).ToList());
        }
    }
}