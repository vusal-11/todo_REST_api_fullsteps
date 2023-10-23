using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_WEB_API.DTOs;
using ToDo_WEB_API.DTOs.Pagination;
using ToDo_WEB_API.Models;
using ToDo_WEB_API.Services;

namespace ToDo_WEB_API.Controllers
{


    /// <summary>
    /// Todo Api main controller
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [Authorize(Policy = "CanView")]
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

        /// <summary>
        /// Create ToDo Item
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Success</response>
        /// <response code="409">Task already created</response>
        /// <response code="403">Forbiden</response>



        //[Authorize (Roles ="admin")]
        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> Post([FromBody] CreatedToDoItemRequest request)
        {
            var createdItem = await _todoService.CreateTodoItem(request);
            return createdItem;
        }


        /// <summary>
        /// Change ToDo Item status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCompleted"></param>
        /// <returns>ToDo task with changed status</returns>



        //[Authorize (Roles ="admin")]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ToDoItemDto>> Patch(int id,[FromBody] bool isCompleted)
        {

            var todoItem = await _todoService.ChangeTodoItemStatusAsync(id,isCompleted);
            return todoItem !=null ? todoItem : NotFound();

        }




    }
}
