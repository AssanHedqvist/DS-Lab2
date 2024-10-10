using ProjectApp.Core;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Persistence;
public class TaskDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdated { get; set; }

    [Required]
    public Status Status { get; set; }

    // FK and navigation property
    [ForeignKey("ProjectId")]
    public ProjectDb ProjectDb { get; set; }

    public int ProjectId { get; set; }
}