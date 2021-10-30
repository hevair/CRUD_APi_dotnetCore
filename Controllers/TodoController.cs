using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class TodoController : ControllerBase
    {



        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDBContext dBContext)
        {
            var todos = await dBContext
                    .Todos
                    .AsNoTracking()
                    .ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDBContext dBContext,
            [FromRoute] int id
        )
        {
            var todo = await dBContext
                    .Todos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);

            return todo == null
                ? NotFound()
                : Ok(todo);
        }

        [HttpPost("todos")] //Outra forma de fazer atribuição de rota
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDBContext dBContext,
            [FromBody] CreateTodoView model
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await dBContext.Todos.AddAsync(todo);
                await dBContext.SaveChangesAsync();

                return Created($"v1/todos/{todo.Id}", todo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDBContext dBContext,
            [FromBody] CreateTodoView model,
            [FromRoute] int id
        )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await dBContext
                .Todos
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

           try
            {

                todo.Title = model.Title;

                dBContext.Todos.Update(todo);
                await dBContext.SaveChangesAsync();

               return Ok(todo);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }


        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDBContext dBContext,
            [FromRoute] int id
        )
        {
            
            var todo = await dBContext
                .Todos
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

           try
            {

                dBContext.Todos.Remove(todo);
                await dBContext.SaveChangesAsync();

               return Ok(todo);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }


        }

    }
}