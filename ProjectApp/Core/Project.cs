namespace ProjectApp.Core;

public class Project
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedDate { get; set; }

    public string UserName { get; set; }

    private List<Task> _tasks = new List<Task>();
    public IEnumerable<Task> Tasks => _tasks;

    public Project(string title, string userName)
    {
        Title = title;
        CreatedDate = DateTime.Now;
        UserName = userName;
    }

    public Project(int id, string title, DateTime createdDate, string userName)
    {
        Id = id;
        Title = title;
        CreatedDate = createdDate;
        UserName = userName;
    }

    public Project() { }

    public void AddTask(Task newTask)
    {
        _tasks.Add(newTask);
    }

    public bool IsCompleted()
    {
        if (_tasks.Count == 0) return true;
        return _tasks.All(t => t.Status == Status.DONE);
    }

    public override string ToString()
    {
        return $"{Id}: {Title} - completed: {IsCompleted()}";
    }
}
