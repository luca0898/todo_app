using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Responses;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Domain.Entities;
using TodoApp.Domain.InputModels;
using TodoApp.Domain.ViewModel;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IMapper _mapper;
        private readonly ITodoService _service;

        public TodoController(
            ILogger<TodoController> logger,
            IMapper mapper,
            ITodoService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync(int currentPage = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            IEnumerable<Todo> entities = await _service.GetManyAsync((entity) => !entity.Deleted, currentPage, pageSize, cancellationToken);

            return Ok(new SuccessResponseViewModel<IEnumerable<TodoViewModel>>(_mapper.Map<IEnumerable<TodoViewModel>>(entities)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            Todo? existingTodo = await _service.GetOneAsync(todo => todo.Id == id, cancellationToken);

            if (existingTodo == null)
            {
                return NotFound(new ErrorResponseViewModel(string.Format("Todo with id {0} not found", id)));
            }

            return Ok(new SuccessResponseViewModel<TodoViewModel>(_mapper.Map<TodoViewModel>(existingTodo)));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] TodoInputModel inputModel, CancellationToken cancellationToken)
        {
            // todo: utilizar mapper
            Todo newTodo = new(inputModel.Title, inputModel.Finished);

            Todo entity = await _service.CreateAsync(newTodo, cancellationToken);

            return Ok(new SuccessResponseViewModel<TodoViewModel>(_mapper.Map<TodoViewModel>(entity)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] TodoInputModel inputModel, CancellationToken cancellationToken)
        {
            Todo? existingTodo = await _service.GetOneAsync(todo => todo.Id == id, cancellationToken);

            if (existingTodo == null)
            {
                return BadRequest(new ErrorResponseViewModel(string.Format("Todo with id {0} not found", id)));
            }

            Todo modifiedTodo = _mapper.Map<Todo>(inputModel);
            modifiedTodo.Id = id;

            await _service.UpdateAsync(modifiedTodo, cancellationToken);

            return Ok(new SuccessResponseViewModel<TodoViewModel>(_mapper.Map<TodoViewModel>(modifiedTodo)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            Todo? existingTodo = await _service.GetOneAsync(todo => todo.Id == id, cancellationToken);

            if (existingTodo == null)
            {
                return BadRequest(new ErrorResponseViewModel(string.Format("Todo with id {0} not found", id)));
            }

            await _service.DestroyAsync(id, cancellationToken);

            return NoContent();
        }
    }
}