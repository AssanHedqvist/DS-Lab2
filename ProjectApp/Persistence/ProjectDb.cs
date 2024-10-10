using System.ComponentModel.DataAnnotations;
 
namespace ProjectApp.Persistence;

public class ProjectDb
{
    // PK
    [Key]
    public int Id { get; set; }
 
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
 
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    // navigation property
    public List<TaskDb> TaskDbs { get; set; } = new List<TaskDb>();
}