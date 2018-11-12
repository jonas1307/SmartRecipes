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
    [Route("api/Ingredients")]
    [Produces("application/json")]
    public class IngredientsController : Controller
    {
        private static List<Ingredient> Ingredients;
        private static int CurrentIdentity;

        public IngredientsController()
        {
            if (Ingredients == null)
            {
                Ingredients = new List<Ingredient>();
                CurrentIdentity = 0;
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Ingredient>), 200)]
        public IActionResult Get([FromQuery]string query)
        {
            var data = (string.IsNullOrEmpty(query) ? Ingredients : Ingredients.Where(f => f.Name.Contains(query)))
                .OrderBy(o => o.Name);

            return Ok(data);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Ingredient), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var data = Ingredients.FirstOrDefault(f => f.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Ingredient), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]Ingredient obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Add(obj);

            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Ingredient), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put([FromRoute]int id, [FromBody]Ingredient obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = Ingredients.FirstOrDefault(f => f.Id == id);

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
            var data = Ingredients.FirstOrDefault(f => f.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            Remove(id);

            return Ok();
        }

        private void Add(Ingredient obj)
        {
            obj.Id = ++CurrentIdentity;

            Ingredients.Add(obj);
        }

        private void Update(int id, Ingredient obj)
        {
            var item = Ingredients.First(f => f.Id == id);

            Ingredients.Remove(item);
            Ingredients.Add(obj);
        }

        private void Remove(int id)
        {
            var item = Ingredients.First(f => f.Id == id);

            Ingredients.Remove(item);
        }
    }
}