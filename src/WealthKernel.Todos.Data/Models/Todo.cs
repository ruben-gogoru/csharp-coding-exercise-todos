namespace WealthKernel.Todos.Data.Models;

public class Todo
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Label { get; set; }

    public bool IsComplete { get; set; }

    internal Todo Clone() => new()
    {
        Id = Id,
        Title = Title,
        Label = Label,
        IsComplete = IsComplete,
    };
}
