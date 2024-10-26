using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Persistence;

public class BaseEntityDb
{ 
    [Key]
    public int Id { get; set; }
}