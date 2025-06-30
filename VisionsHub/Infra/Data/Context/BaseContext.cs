using Microsoft.EntityFrameworkCore;

namespace VisionsHub.Infra.Data.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }
    }
}
