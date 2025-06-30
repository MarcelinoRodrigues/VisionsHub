using Microsoft.EntityFrameworkCore;
using VisionsHub.Domain.Entities;

namespace VisionsHub.Infra.Data.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        public DbSet<Book> Livros { get; set; }
        public DbSet<Student> Alunos { get; set; }
        public DbSet<Loan> Emprestimos { get; set; }
    }
}
