using LibraryModel.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryModel.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Attendances> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Managers>().ToTable("Managers");         
            modelBuilder.Entity<Employees>().ToTable("Employees");
            modelBuilder.Entity<Departments>().ToTable("Departments");
            modelBuilder.Entity<Attendances>().ToTable("Attendances");


        }
    }
}