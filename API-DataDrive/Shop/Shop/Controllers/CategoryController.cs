using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

//Endpoint => URL

namespace Shop.Controllers
{
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Category>> Get()
        {
            return new Category();
        }

        [HttpGet]
        [Route("{id:Int}")]
        public async Task<ActionResult<List<Category>>> GetById(int id)
        {
            return new List<Category>();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody]Category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id,[FromBody]Category model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Category>> Deletet()
        {
            return Ok();
        }
    }
}
