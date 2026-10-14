using System.Collections.Concurrent;
using TasksApi.Models;

namespace TasksApi.Services;

public interface ITaskStore
{
    IEnumerable<TaskItem> All();
    TaskItem? Find(int id);
    TaskItem Add(string title);
    TaskItem? Complete(int id);
    bool Remove(int id);
}

public class InMemoryTaskStore : ITaskStore
{
    private readonly ConcurrentDictionary<int, TaskItem> _items = new();
    private int _nextId;

    public IEnumerable<TaskItem> All() => _items.Values.OrderBy(t => t.Id);

    public TaskItem? Find(int id) => _items.TryGetValue(id, out var t) ? t : null;

    public TaskItem Add(string title)
    {
        var item = new TaskItem(Interlocked.Increment(ref _nextId), title, false);
        _items[item.Id] = item;
        return item;
    }

    public TaskItem? Complete(int id)
    {
        if (!_items.TryGetValue(id, out var t)) return null;
        var done = t with { Done = true };
        _items[id] = done;
        return done;
    }

    public bool Remove(int id) => _items.TryRemove(id, out _);
}
