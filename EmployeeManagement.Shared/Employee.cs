using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Shared.Models
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อ")]
        [MaxLength(100)]
        public string? Employee_First_name { get; set; }

        [Required(ErrorMessage = "กรุณากรอกนามสกุล")]
        [MaxLength(100)]
        public string? Employee_Last_name { get; set; }

        [MaxLength(100)]
        public string? Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_Birth { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Joined { get; set; }

        [Column(TypeName = "text")]
        public string? Employee_Address { get; set; }
        public string? Photo { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกแผนก")]
        [Range(1, int.MaxValue, ErrorMessage = "กรุณาเลือกแผนกที่ถูกต้อง")]
        public int Department_ID { get; set; }

        [ForeignKey("Department_ID")]
        public virtual Department? Department { get; set; }
    }
}