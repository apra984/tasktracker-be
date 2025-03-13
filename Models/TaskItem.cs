using System.ComponentModel.DataAnnotations;
using TaskTracker.Api.Enums;

namespace TaskTracker.Api.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public TaskItemStatus Status { get; set; }
}
