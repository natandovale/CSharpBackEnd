using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System;
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
        public async Task<ActionResult<List<Category>>> Get([FromServices]DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id:Int}")]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return category;

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel criar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model, [FromServices] DataContext context)
        {
            //Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });
            //Verifica se os dados são validos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel atualizar a categoria" });
            }

            return Ok(model);
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices]DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria excluida como sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }
        }
    }
}
