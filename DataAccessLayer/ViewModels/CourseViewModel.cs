using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class CourseViewModel
{
    public int Id { get; set; }

    [Required]
    public string CourseName { get; set; }

    [Required]
    public string Content { get; set; } 

    [Required]
    [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6.")]
    public int Credits { get; set; }

     [Required]
    public int DepartmentId {get; set;}

    [Required]
    public string Department { get; set; } 

    public bool isAvailable {get; set;}

}
