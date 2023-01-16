using System.ComponentModel.DataAnnotations;

namespace LibraryModel.Models
{
    public class Employees
    {
        
        public int ID { get; set; }
        [StringLength(50), Required]
        public string? EmployeeName { get; set; }
        [Range(18,65,ErrorMessage = "The age must be between 18 and 65 ..."), Required]
        public int Age { get; set; }
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [StringLength(50), Required]
        public string? JobTitle { get; set; }
        [StringLength(10), Required]
        public string? Salary { get; set; }
        [StringLength(50), Required]
        public string? Department { get; set; }
        public Departments? Departments { get; set; }
        public ICollection<Attendances>? Attendances { get; set; }



    }
}