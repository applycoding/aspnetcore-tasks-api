using System.ComponentModel.DataAnnotations;

namespace TasksApi.Models;

public record TaskItem(int Id, string Title, bool Done);

public record CreateTaskRequest([Required, StringLength(200, MinimumLength = 1)] string Title);
