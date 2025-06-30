using Microsoft.EntityFrameworkCore;
using VisionsHub.Domain.Entities;

namespace VisionsHub.Infra.Data.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Loan> Loan { get; set; }
    }
}
