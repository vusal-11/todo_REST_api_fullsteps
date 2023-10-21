using ToDo_WEB_API.Data;
using ToDo_WEB_API.DTOs;
using ToDo_WEB_API.Models;

namespace ToDo_WEB_API.Services;

public class TodoService : ITodoService
{

    //private readonly Dictionary<int, ToDoItem> _items = new();
    //private int _nextId = 1;


    private readonly ToDoDbContext _dbContext;

    public TodoService(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ToDoItemDto?> ChangeTodoItemStatusAsync(int id, bool isCompleted)
    {
        
        var item = await _dbContext.ToDoItems.FindAsync(id);
        if (item is null)
        {
            return null;
        }

        item.IsCompleted = isCompleted; 
        item.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return ConvertTodoItemDto(item);




    }

    public async Task<ToDoItemDto> CreateTodoItem(CreatedToDoItemRequest request)
    {

        var now = DateTime.UtcNow;
        var item = new ToDoItem()
        {
            Text = request.Text,
            CreatedAt = now,
            UpdatedAt = now,
            IsCompleted = false
        };

         item = _dbContext.Add(item).Entity;
         await _dbContext.SaveChangesAsync();
        return ConvertTodoItemDto(item);

    }

    public async Task<ToDoItemDto?> GetToDoItemAsync(int id)
    {
        var item =await _dbContext.ToDoItems.FindAsync(id);
        return item is not null ? ConvertTodoItemDto(item) : null;
        
    }

    public  Task<IEnumerable<ToDoItemDto>> GetToDoItemsAsync()
    {
        var items = _dbContext.ToDoItems.ToList();
        return Task.FromResult<IEnumerable<ToDoItemDto>>(items.Select(item => ConvertTodoItemDto(item)));

    }


    public ToDoItem ConvertTodoItem(ToDoItemDto item)
    {
        var todoItem = new ToDoItem()
        {
            Id = item.Id,
            Text = item.Text,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.CreatedAt,
            IsCompleted = item.IsCompleted,
        };
        return todoItem;
    }

    public ToDoItemDto ConvertTodoItemDto(ToDoItem item)
    {
        var todoItemDto = new ToDoItemDto()
        {
            Id = item.Id,
            Text = item.Text,
            CreatedAt = item.CreatedAt,
            IsCompleted = item.IsCompleted,
        };
        return todoItemDto;
    }





}
