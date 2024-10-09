using ProjectApp.Core.Interfaces;

namespace ProjectApp.Core;

public class MockProjectService : IProjectService
{
    public List<Project> GetAllByUserName(string userName)
    {
        return _projects;
    }

    public Project GetById(int id, string userName)
    {
        return _projects.Find(p => p.Id == id && p.UserName == userName);
    }

    public void Add(string userName, string Title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }

    private static readonly List<Project> _projects = new();

    // C# style static initilalizer
    static MockProjectService()
    {
        Project p1 = new Project(1, "Learn ASP:NET with MVC", DateTime.Now, "anderslm@kth.se");
        Project p2 = new Project(2, "Prepare for your Bachelor Thesis", DateTime.Now, "anderslm@kth.se");
        p2.AddTask(new Core.Task(1, "Find an interesting topic and company"));
        p2.AddTask(new Core.Task(1, "Start a pre-study"));
        _projects.Add(p1);
        _projects.Add(p2);
    }
}