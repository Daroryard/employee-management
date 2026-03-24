using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared.Models
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อแผนก")]
        [MaxLength(100)]
        public string? Department_Name { get; set; }
    }
}