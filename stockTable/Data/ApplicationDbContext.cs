using stockTable.Models;
using Microsoft.EntityFrameworkCore;


namespace stockTable.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Equipment> Equipments { get; set; } 
        public DbSet<Status> Statuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasIndex(e => e.InventoryNum).IsUnique();
        }
    }
}
