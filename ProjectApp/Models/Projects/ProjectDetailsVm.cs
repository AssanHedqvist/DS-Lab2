using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Projects;

public class ProjectDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    public string Title { get; set; }

    [Display(Name = "Created date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime CreatedDate { get; set; }

    public bool IsCompleted { get; set; }
    
    public List<TaskVm> TaskVMs { get; set; } = new();

    public static ProjectDetailsVm FromProject(Project project)
    {
        var detailsVm = new ProjectDetailsVm()
        {
            Id = project.Id,
            Title = project.Title,
            CreatedDate = project.CreatedDate,
            IsCompleted = project.IsCompleted()

        };
        foreach(var task in project.Tasks)
        {
            detailsVm.TaskVMs.Add(TaskVm.FromTask(task));
        }

        return detailsVm;
    }
}