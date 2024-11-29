using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BudgetAssistent.Models;

namespace BudgetAssistent.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SavingsGoal> SavingsGoal { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
    }
}
