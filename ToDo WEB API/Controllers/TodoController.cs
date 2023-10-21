using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_WEB_API.DTOs;
using ToDo_WEB_API.DTOs.Pagination;
using ToDo_WEB_API.Models;
using ToDo_WEB_API.Services;

namespace ToDo_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationListDto<ToDoItemDto>>> Get(
            [FromQuery] ToDoQueryFilters filters,
            [FromQuery] PaginationRequest request
            )
        {
            return await _todoService.GetToDoItemsAsync(
                request.Page,
                request.PageSize,
                filters.Search,
                filters.IsCompleted
                );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDto>> Get(int id)
        {
            var item = await _todoService.GetToDoItemAsync(id);
            return item != null ? item : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> Post([FromBody] CreatedToDoItemRequest request)
        {
            var createdItem = await _todoService.CreateTodoItem(request);
            return createdItem;
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ToDoItemDto>> Patch(int id,[FromBody] bool isCompleted)
        {

            var todoItem = await _todoService.ChangeTodoItemStatusAsync(id,isCompleted);
            return todoItem !=null ? todoItem : NotFound();

        }




    }
}
