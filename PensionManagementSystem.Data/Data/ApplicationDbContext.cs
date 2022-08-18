using Microsoft.EntityFrameworkCore;
using PensionManagementSystem.Models;

namespace PensionManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PensionerDetail> PensionerDetails { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id=1, Username="User1", Password= "VXNlcjE=" },
                new User { Id = 2, Username = "User2", Password = "VXNlcjI=" }
                );

            modelBuilder.Entity<Bank>().HasData(
                new Bank { Id =1, AccountNumber=9876543210,Type=0},
                new Bank { Id = 2, AccountNumber = 7894561230, Type = 0 }
                );

            modelBuilder.Entity<PensionerDetail>().HasData(
                new PensionerDetail { UserId =1, Name="User one", DateOfBirth = DateTime.Now, PAN = "PAN9876543", AadhaarNumber=123456789012,SalaryEarned=30000,Allowances=5000,Type=0,BankId=1},
                new PensionerDetail { UserId =2, Name="User two", DateOfBirth = DateTime.Now.AddDays(1), PAN = "PAN7894562", AadhaarNumber=123456789013,SalaryEarned=32000,Allowances=4500,Type=0,BankId=2}
                );
        }
    }
}
