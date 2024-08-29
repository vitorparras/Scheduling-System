using Domain.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Store : BaseEntity
    {
        [Required]
        public Guid AdminId { get; set; }

        [Required]
        public Guid CalendarId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(2000)]
        public string GoogleAddress { get; set; }

        [Required]
        [StringLength(2000)]
        public string Link { get; set; }

        public virtual User Admin { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Admin)
                .WithMany(u => u.Stores)
                .HasForeignKey(s => s.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Calendar)
                .WithOne(c => c.Store) 
                .HasForeignKey<Store>(s => s.CalendarId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Employees)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Appointments)
                .WithOne(a => a.Store)
                .HasForeignKey(a => a.StoreId)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
