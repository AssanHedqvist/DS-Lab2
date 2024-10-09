namespace ProjectApp.Core.Interfaces;

public interface IProjectPersistence
{
    List<Project> GetAllByUserName(string userName);
    
    Project GetById(int id, string userName);
    
    void Save(Project project);
}