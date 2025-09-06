using ToDoApp.DTOs;

namespace ToDoApp.Services;

public interface IToDoService
{
    Task<ToDoItemDTO> ChangeToDoStatusAsync(int id, bool isCompleted);
    Task<ToDoItemDTO> CreateToDoAsync(ToDoItemCreateDTO request);
    Task<ToDoItemDTO> GetToDoItemAsync(int id);
    Task<IEnumerable<ToDoItemDTO>> GetToDoItemsAsync();
}
