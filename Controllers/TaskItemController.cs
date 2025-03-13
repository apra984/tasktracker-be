using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Services;

namespace TaskTracker.Controllers;

/// <summary>
/// This API controller is responsible for managing CRUD operations on tasks
/// </summary>
[ApiController]
[Route("api")]
public class TaskItemController : ControllerBase
{
    private readonly ILogger<TaskItemController> _logger;
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ILogger<TaskItemController> logger, ITaskItemService taskItemService)
    {
        _logger = logger;
        _taskItemService = taskItemService;
    }

    /// <summary>
    /// Retrieve all tasks from the in-memory db
    /// </summary>
    /// <returns>A list of tasks</returns>
    /// <response code="200">Returns the list of tasks</response>
    /// <response code="500">Unexpected error occured on the server</response>
    [HttpGet]
    [Route("tasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        //TODO: add filter by name, sort order, pagination

        var tasks = await _taskItemService.GetTaskItems();
        return Ok(tasks);
    }

    /// <summary>
    /// Retrieve a single task by Id
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns>A single task</returns>
    /// <response code="200">Returns the requested task</response>
    /// <response code="404">Task was not found</response>
    /// <response code="500">Unexpected error occured on the server</response>
    [HttpGet]
    [Route("task/{taskId}")]
    public async Task<IActionResult> GetTask(int taskId)
    {
        var task = await _taskItemService.GetTaskItem(taskId);

        if (task is null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    /// <summary>
    /// Creates a new task with auto-incrementing id
    /// </summary>
    /// <param name="taskDto"></param>
    /// <returns>The new task id</returns>
    /// <response code="200">Returns the new task id</response>
    /// <response code="500">Unexpected error occured on the server</response>
    [HttpPost]
    [Route("createTask")]
    public async Task<IActionResult> CreateTask([FromBody] TaskItemDto taskDto)
    {
        var taskId = await _taskItemService.CreateTask(taskDto);
        return Ok(new { TaskId = taskId});
    }

    /// <summary>
    /// Deletes an existing task by task Id
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns>No content</returns>
    /// <response code="200">No content</response>
    /// <response code="404">Task not found</response>
    /// <response code="500">Unexpected error occured on the server</response>
    [HttpDelete]
    [Route("deleteTask/{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var result = await _taskItemService.DeleteTask(taskId);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new { Message = $"Task: {taskId} deleted successfully." });
    }

    [HttpPut]
    [Route("updateTask")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskItemDto taskDto)
    {
        var result = await _taskItemService.UpdateTask(taskDto);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new { Message = $"Task: {taskDto.Id} updated successfully." });
    }
}
