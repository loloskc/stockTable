using stockTable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace stockTable.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Equipment> Equipments { get; set; } 
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>().HasIndex(e => e.InventoryNum).IsUnique();

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "в эксплуатации",Color = "table-success" },
                new Status { Id = 2, Name = "подготовка к списанию", Color = "table-warning" },
                new Status { Id = 3, Name = "подготовка к утилизации", Color = "table-danger" }
                );
        }
    }
}
