using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WealthKernel.Todos.Data.Models;

namespace WealthKernel.Todos.Data;

public class InMemoryTodosRepository
{
    private readonly ConcurrentDictionary<string, Todo> _todos = new();

    public Task<Todo> GetTodo(string id)
    {
        var todo = _todos.GetValueOrDefault(id);

        return Task.FromResult(todo?.Clone());
    }

    public Task<IList<Todo>> GetTodos(string label = null, bool? isComplete = null)
    {
        var todos = _todos.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(label)) todos = todos.Where(t => t.Label == label);
        if (isComplete.HasValue) todos = todos.Where(t => t.IsComplete == isComplete.Value);

        return Task.FromResult<IList<Todo>>(todos.Select(t => t.Clone()).ToArray());
    }

    public Task AddTodo(Todo todo)
    {
        var added = _todos.TryAdd(todo.Id, todo);

        if (!added)
        {
            throw new InvalidOperationException("Todo could not be added.");
        }

        return Task.CompletedTask;
    }

    public Task UpdateTodo(Todo todo)
    {
        if (!_todos.TryGetValue(todo.Id, out var previousTodoValue))
        {
            throw new InvalidOperationException("Todo does not exist.");
        }

        _todos.TryUpdate(todo.Id, todo, previousTodoValue);

        return Task.CompletedTask;
    }
}
