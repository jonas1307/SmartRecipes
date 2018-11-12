using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartRecipies.Backend.Models;

namespace SmartRecipies.Backend.Controllers
{
    [AllowAnonymous]
    [EnableCors("DefaultPolicy")]
    [Route("api/Recipes")]
    [Produces("application/json")]
    public class RecipesController : Controller
    {
        private static List<Recipe> Recipes;
        private static int CurrentIdentity;

        public RecipesController()
        {
            if (Recipes == null)
            {
                Recipes = new List<Recipe>();
                CurrentIdentity = 0;
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Recipe>), 200)]
        public IActionResult Get([FromQuery]string query)
        {
            var data = (string.IsNullOrEmpty(query) ? Recipes : Recipes.Where(f => f.Name.Contains(query)))
                .OrderBy(o => o.Name);

            return Ok(data);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Recipe), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var data = Recipes.FirstOrDefault(f => f.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Recipe), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]Recipe obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Add(obj);

            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Recipe), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put([FromRoute]int id, [FromBody]Recipe obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = Recipes.FirstOrDefault(f => f.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            Update(id, obj);

            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var data = Recipes.FirstOrDefault(f => f.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            Remove(id);

            return Ok();
        }

        private void Add(Recipe obj)
        {
            obj.Id = ++CurrentIdentity;

            Recipes.Add(obj);
        }

        private void Update(int id, Recipe obj)
        {
            var item = Recipes.First(f => f.Id == id);

            Recipes.Remove(item);
            Recipes.Add(obj);
        }

        private void Remove(int id)
        {
            var item = Recipes.First(f => f.Id == id);

            Recipes.Remove(item);
        }
    }
}