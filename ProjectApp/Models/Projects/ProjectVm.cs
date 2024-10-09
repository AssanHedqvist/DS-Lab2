using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Projects;

public class ProjectVm
{
    [ScaffoldColumn(false)] public int Id { get; set; }

    public string Title { get; set; }

    [Display(Name = "Created Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime CreatedDate { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public static ProjectVm FromProject(Project project)
    {
        return new ProjectVm
        {
            Id = project.Id,
            Title = project.Title,
            CreatedDate = project.CreatedDate,
            IsCompleted = project.IsCompleted()
        };
    }


}