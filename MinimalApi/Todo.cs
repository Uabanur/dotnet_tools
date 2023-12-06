
public class Todo 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public bool Completed { get; set; }
}


public class Todos
{
    private readonly List<Todo> store = new();

    public void Add(Todo entry)
    {
        store.Add(new Todo
        {
            Id = Guid.NewGuid(),
            Name = entry.Name,
            Completed = entry.Completed,
        });
    }

    public void Update(Guid id, Todo update)
    {
        var entry = Get(id);
        if (entry is null) return;

        entry.Name = update.Name;
        entry.Completed = update.Completed;
    }

    public int Delete(Guid id) 
        => store.RemoveAll(x => x.Id == id);

    public Todo? Get(Guid id) 
        => store.Find(x => x.Id == id);

    public IReadOnlyCollection<Todo> Complete() 
        => store.Where(x => x.Completed).ToList().AsReadOnly<Todo>();

    public IReadOnlyCollection<Todo> List() 
        => store.AsReadOnly<Todo>();
}