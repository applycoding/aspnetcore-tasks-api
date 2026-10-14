using Microsoft.AspNetCore.Mvc;
using TasksApi.Models;
using TasksApi.Services;

namespace TasksApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(ITaskStore store) : ControllerBase
{
    [HttpGet]
    public IEnumerable<TaskItem> List() => store.All();

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> Get(int id) => store.Find(id) is { } t ? t : NotFound();

    [HttpPost]
    public ActionResult<TaskItem> Create(CreateTaskRequest req)
    {
        var item = store.Add(req.Title);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPost("{id:int}/complete")]
    public ActionResult<TaskItem> Complete(int id) => store.Complete(id) is { } t ? t : NotFound();

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) => store.Remove(id) ? NoContent() : NotFound();
}
