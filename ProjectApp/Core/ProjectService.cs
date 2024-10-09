using System.Data;
using ProjectApp.Core.Interfaces;


namespace ProjectApp.Core;

public class ProjectService : IProjectService
{
    private readonly IProjectPersistence _projectPersistence;

    public ProjectService(IProjectPersistence projectPersistence)
    {
        _projectPersistence = projectPersistence;
    }
    
    public List<Project> GetAllByUserName(string userName)
    {
        List<Project> projects = _projectPersistence.GetAllByUserName(userName);
        return projects;
    }

    public Project GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Project GetById(int id, string userName)
    {
        Project project = _projectPersistence.GetById(id, userName);
        if(project == null) throw new DataException("project not found");
        return project;
    }

    public void Add(string userName, string title)
    {
        throw new NotImplementedException("Add");
    }
}