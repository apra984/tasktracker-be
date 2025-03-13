using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Models;

namespace TaskTracker.Api.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ILogger<TaskItemService> _logger;
    private readonly ConnectorDbContext _context;

    public TaskItemService(ILogger<TaskItemService> logger, ConnectorDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<int> CreateTask(TaskItemDto taskDto)
    {
        var taskList = await _context.Tasks.ToListAsync();
        var currentTaskCount = taskList.Count != 0 ? taskList.Max(t => t.Id) : 0;

        var task = new TaskItem
        {
            Id = currentTaskCount+1,
            Name = taskDto.Name,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            Status = taskDto.Status
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        
        return task.Id;
    }

    public async Task<int?> DeleteTask(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);

        if (task is null)
        {
            _logger.LogError("Task not found.");
            return null;
        }

        _context.Remove(task);
        await _context.SaveChangesAsync();
        return task.Id;
    }

    public async Task<TaskItemDto?> GetTaskItem(int taskId)
    {
        var task = await _context.Tasks.Where(t => t.Id == taskId)
                                   .Select(_ => new TaskItemDto(_))
                                   .FirstOrDefaultAsync();
                                  

        if (task is null)
        {
            _logger.LogError("Task: {@TaskId} not found.", taskId);
            return null;
        }

        return task;
    }

    public async Task<IEnumerable<TaskItemDto>> GetTaskItems()
    {
        var taskList = await _context.Tasks.Select(_ => new TaskItemDto(_))
                                           .AsNoTracking()
                                           .ToListAsync();

        _logger.LogInformation("Found {@TaskCount} tasks.", taskList.Count);

        return taskList;
    }

    public async Task<int?> UpdateTask(TaskItemDto taskDto)
    {
        var task = await _context.Tasks.FindAsync(taskDto.Id);

        if (task is null)
        {
            _logger.LogError("Task not found.");
            return null;
        }

        _context.Entry(task).CurrentValues.SetValues(taskDto);
        await _context.SaveChangesAsync();
        
        return task.Id;
    }
}
