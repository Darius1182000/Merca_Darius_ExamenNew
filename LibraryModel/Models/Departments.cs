using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace LibraryModel.Models
{
    
    public class Departments {

    
    public int ID { get; set; }
        [StringLength(50), Required]
    public string? Department { get; set; }
        [StringLength(50), Required]
    public string? Manager { get; set; }

    public ICollection<Managers>? Managers { get; set; }
    public ICollection<Employees>? Employees { get; set; }

}
}