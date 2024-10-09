using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;
using Task = ProjectApp.Core.Task;

namespace ProjectApp.Models.Projects;

public class TaskVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    public string Description { get; set; }

    [Display(Name = "Created date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime LastUpdated { get; set; }

    public Status Status { get; set; }

    public static TaskVm FromTask(Task task)
    {
        return new TaskVm()
        {
            Id = task.Id,
            Description = task.Description,
            LastUpdated = task.LastUpdated,
            Status = task.Status
        };
    }
}