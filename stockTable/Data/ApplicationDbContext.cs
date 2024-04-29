using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stockTable.Models;


namespace stockTable.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LowEquipment> LowEquipments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "в эксплуатации", Color = "table-success" },
                new Status { Id = 2, Name = "подготовка к списанию", Color = "table-warning" },
                new Status { Id = 3, Name = "подготовка к утилизации", Color = "table-danger" },
                new Status { Id = 4, Name = "требуется ремонт", Color = "table-secondary" },
                new Status { Id = 5, Name = "резерв", Color = "table-primary" }
                );
        }
    }
}
