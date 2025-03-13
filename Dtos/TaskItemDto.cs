using TaskTracker.Api.Models;

namespace TaskTracker.Api.Dtos;

public class TaskItemDto : TaskItem
{
    public TaskItemDto()
    {
    }

    public TaskItemDto(TaskItem task)
    {
        Id = task.Id;
        Name = task.Name;
        Description = task.Description;
        DueDate = task.DueDate;
        Status = task.Status;
    }
}
