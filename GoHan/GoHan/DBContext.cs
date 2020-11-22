using GoHan.Models;
using Microsoft.EntityFrameworkCore;

namespace GoHan
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<DBInitialModel> DBInit { get; set; }
        public DbSet<UserInfoModel> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<DBInitialModel>().
            ToTable("test");
            modelBuilder
            .Entity<UserInfoModel>().
            ToTable("userinfo");

        }
    }
}
