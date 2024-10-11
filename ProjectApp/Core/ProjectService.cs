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
       return _projectPersistence.GetAllByUserName(userName);
    }

    public Project GetById(int id)
    {
        throw new NotImplementedException("GetById");
    }

    public Project GetById(int id, string userName)
    {
        Project project = _projectPersistence.GetById(id, userName);
        return project;
    }

    public void Add(string userName, string title)
    {
        throw new NotImplementedException("Add");
    }
}