using ToDoApp.Data;
using ToDoApp.DTOs;
using ToDoApp.Models;

namespace ToDoApp.Services;

public class ToDoService: IToDoService
{
    private readonly AppDbContext _context;

    public ToDoService(AppDbContext context)
    {
        _context = context;
    }

    public Task<ToDoItemDTO> ChangeToDoStatusAsync(int id, bool isCompleted)
    {
        var item = _context.ToDoItems.FirstOrDefault(t => t.Id == id)
              ?? throw new KeyNotFoundException($" {id} not found.");

        item.IsCompleted = isCompleted;
        _context.SaveChanges();
        return Task.FromResult(ConvertToDoItemDTO(item));
    }

    public Task<ToDoItemDTO> CreateToDoAsync(ToDoItemCreateDTO request)
    {
        var item = new ToDoItem()
        {
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsCompleted = false
        };
        _context.ToDoItems.Add(item);
        _context.SaveChanges();

        return Task.FromResult(ConvertToDoItemDTO(item));
    }

    public Task<ToDoItemDTO> GetToDoItemAsync(int id)
    {
        var item = _context.ToDoItems.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(ConvertToDoItemDTO(item));
    }

    public Task<IEnumerable<ToDoItemDTO>> GetToDoItemsAsync()
    {
        var items = _context.ToDoItems.ToList();
        return Task.FromResult(items.Select(ConvertToDoItemDTO));
    }

    private ToDoItemDTO ConvertToDoItemDTO(ToDoItem item)
    {
        ToDoItemDTO itemDTO = new()
        {
            Id = item.Id,
            Text = item.Text,
            CreatedAt = item.CreatedAt,
            isCompleted = item.IsCompleted
        };
        return itemDTO;
    }
}
