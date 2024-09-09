using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using TunApi.Models;
using TunApi.Data;

namespace TunApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            var todos = new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "Learn C#",
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new Todo
                {
                    Id = 2,
                    Title = "Learn ASP.NET Core",
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                }
            };

            todos = await _context.Todo.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTodoById(int id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from claims
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email); // Get email

            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"Username: {userName}");
            Console.WriteLine($"Email: {email}");

            var todo = await _context.Todo
                .Where(t => t.Id == id)
                .Include(t => t.TodoFiles)
                .FirstOrDefaultAsync();

            if (todo == null)
            {
                return NotFound();
            }

            Console.WriteLine("TODO: ");
            Console.WriteLine(todo.Id);
            Console.WriteLine(todo.Title);

            foreach (var todoFile in todo.TodoFiles)
            {
                Console.WriteLine($"Child Id: {todoFile.Id}, FilePath: {todoFile.FilePath}");
            }

            var todoDto = new TodoDto(todo);

            return Ok(todoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(Todo todo)
        {
            todo.CreatedAt = DateTime.Now.ToUniversalTime();
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoById", new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, Todo todo)
        {
            var dbTodo = await _context.Todo.FindAsync(id);
            if (dbTodo == null)
            {
                return NotFound("Todo not found");
            }

            dbTodo.Title = todo.Title;
            dbTodo.IsCompleted = todo.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(await _context.Todo.FindAsync(id));
        }
    }
}