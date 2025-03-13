using TaskTracker.Api.Dtos;

namespace TaskTracker.Api.Services;

public interface ITaskItemService
{
    Task<TaskItemDto?> GetTaskItem(int taskId);
    Task<IEnumerable<TaskItemDto>> GetTaskItems();
    Task<int> CreateTask(TaskItemDto taskDto);
    Task<int?> UpdateTask(TaskItemDto taskId);
    Task<int?> DeleteTask(int taskId);
}
