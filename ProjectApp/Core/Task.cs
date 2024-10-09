namespace ProjectApp.Core;

public class Task
{
    public int Id { get; set; }

    public string Description { get; set; }

    private DateTime _lastUpdated;
    public DateTime LastUpdated { get => _lastUpdated; }

    private Status _status;
    public Status Status
    {
        get => _status;
        set
        {
            if (_status == Status.DONE && value != Status.DONE) 
                throw new InvalidOperationException("item is done");
            _status = value;
            _lastUpdated = DateTime.Now;
        }
    }

    public Task(string descr, Status status = Status.TO_DO)
    {
        Description = descr;
        _lastUpdated = DateTime.Now;
        _status = status;
    }

    public Task(int id, string descr, Status status = Status.TO_DO)
    {
        Id = id;
        Description = descr;
        _lastUpdated = DateTime.Now;
        _status = status;
    }

    public Task(int id, string descr, DateTime lastUpdated, Status status = Status.TO_DO)
    {
        Id = id;
        Description = descr;
        _lastUpdated = lastUpdated;
        _status = status;
    }

    public Task() { }

    public override string ToString()
    {
        return $"{Id}: {Description} - {Status}";
    }
}