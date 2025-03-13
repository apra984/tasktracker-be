using System.Runtime.Serialization;

namespace TaskTracker.Api.Enums;

public enum TaskItemStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 0,
    [EnumMember(Value = "InProgress")]
    InProgress = 1,
    [EnumMember(Value = "Completed")]
    Completed = 2
}
