using System.ComponentModel.DataAnnotations;

namespace LibraryModel.Models
{
    public class Attendances
    {
        
        public int ID { get; set; }
        [StringLength(50), Required]
        public string? EmployeeName { get; set; }    
        [DataType(DataType.Date), Required]
        public DateTime Date { get; set; }


        [StringLength(5)]
        public string? InTime { get; set; }
        [StringLength(5)]
        public string? OutTime { get; set; }

        [StringLength(70)]
        public string? AbsenceReason { get; set; }
        public Employees? Employee { get; set; }

    }
}
