using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models.Projects;

public class CreateProjectVm
{
    [Required]
    [StringLength(128, ErrorMessage = "Max length is 128 characters")]
    public string Title { get; set; }
}