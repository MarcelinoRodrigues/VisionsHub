using VisionsHub.Domain.Entities;
using VisionsHub.Infra.Data.Context;

namespace VisionsHub.Infra.Repository
{
    public class StudentRepository
    {
        private readonly BaseContext _context;

        public StudentRepository(BaseContext context)
        {
            _context = context;
        }
        public async Task<Student?> GetByIdAsync(Guid studentId)
        {
            return await _context.Student.FindAsync(studentId);
        }
    }
}
