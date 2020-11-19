using GoHan.Models;
using Microsoft.EntityFrameworkCore;

namespace GoHan
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<DBInitialModel> DBInit { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<DBInitialModel>().
            ToTable<DBInitialModel>("test");
        }
    }
}
