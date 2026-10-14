using Xunit;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TasksApi.Models;
using TasksApi.Services;

namespace TasksApi.Tests;

public class InMemoryTaskStoreTests
{
    [Fact]
    public void AddThenComplete_MarksTaskDone()
    {
        var store = new InMemoryTaskStore();
        var t = store.Add("write tests");
        Assert.True(store.Complete(t.Id)!.Done);
    }

    [Fact]
    public void Remove_MissingTask_ReturnsFalse() => Assert.False(new InMemoryTaskStore().Remove(42));
}

public class TasksEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Post_CreatesTask()
    {
        var res = await _client.PostAsJsonAsync("/api/tasks", new { title = "ship it" });
        Assert.Equal(HttpStatusCode.Created, res.StatusCode);
        var item = await res.Content.ReadFromJsonAsync<TaskItem>();
        Assert.Equal("ship it", item!.Title);
    }

    [Fact]
    public async Task Post_EmptyTitle_ReturnsBadRequest()
    {
        var res = await _client.PostAsJsonAsync("/api/tasks", new { title = "" });
        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
    }

    [Fact]
    public async Task Get_Missing_Returns404()
    {
        var res = await _client.GetAsync("/api/tasks/9999");
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        var res = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}
