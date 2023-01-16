using System.ComponentModel.DataAnnotations;

namespace LibraryModel.Models
{
    public class Managers
    {
        
        public int ID { get; set; }
        [StringLength(50), Required ]
        public string? Manager { get; set; }
        [StringLength(50), Required]
        public string? Department { get; set; }


        //refference navigation property
        public Departments? Departments { get; set; }
    }
}
