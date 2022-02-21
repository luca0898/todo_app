using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Entities;
using TodoApp.Models.InputModels;
using TodoApp.Models.ViewModel;
using TodoApp.Repositories;
using System.Linq;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        private readonly InMemoryDbContext _db;
        private readonly ILogger<TodoController> _logger;

        public TodoController(InMemoryDbContext db, ILogger<TodoController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (_db.TodoList == null)
            {
                return BadRequest("TodoList is empty");
            }

            return Ok(new { data = _db.TodoList.ToList() });
        }

        [HttpGet("{id}")]
        public IActionResult GetOne([FromRoute] string id)
        {
            if (_db.TodoList == null)
            {
                return BadRequest("TodoList is empty");
            }

            Todo? existingTodo = _db.TodoList
                .AsQueryable()
                .FirstOrDefault(todo => todo.Id == id);

            if (existingTodo == null)
            {
                return BadRequest(string.Format("Todo with id {0} not found", id));
            }

            return Ok(new TodoViewModel(existingTodo?.Id ?? "", existingTodo?.Title ?? "", existingTodo?.Finished ?? false));
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] TodoInputModel inputModel)
        {
            Todo newTodo = new(inputModel.Title, inputModel.Finished);

            _db.TodoList?.Add(newTodo);

            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] TodoInputModel inputModel)
        {
            if (_db.TodoList == null)
            {
                return BadRequest("TodoList is empty");
            }

            Todo? existingTodo = _db.TodoList
                .AsQueryable()
                .FirstOrDefault(todo => todo.Id == id);

            if (existingTodo == null)
            {
                return BadRequest(string.Format("Todo with id {0} not found", id));
            }

            Todo modifiedTodo = new(inputModel.Title, inputModel.Finished) { Id = id };

            _db.Entry(modifiedTodo).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(new TodoViewModel(modifiedTodo.Id, modifiedTodo.Title, modifiedTodo.Finished));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            if (_db.TodoList == null)
            {
                return BadRequest("TodoList is empty");
            }

            Todo? existingTodo = _db.TodoList
                .AsQueryable()
                .FirstOrDefault(todo => todo.Id == id);

            if (existingTodo == null)
            {
                return BadRequest(string.Format("Todo with id {0} not found", id));
            }

            _db.TodoList?.Remove(existingTodo);

            _db.SaveChanges();

            return NoContent();
        }
    }
}