using Api.models;
using Microsoft.EntityFrameworkCore;

namespace Api.data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }
        public DbSet<Vehicle> vehiclesTbl { get; set; }
        public DbSet<Image> ImageTbl { get; set; }
        public DbSet<User> UserTbl { get; set; }
        public DbSet<Category> CategoryTbl { get; set; }
        
    }
}

