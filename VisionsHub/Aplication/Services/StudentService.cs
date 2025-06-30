using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;
using VisionsHub.Infra.Repository;

namespace VisionsHub.Aplication.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository repository)
        {
            _studentRepository = repository;
        }
        public async Task Create(StudentRequest request)
        {
            await _studentRepository.CreateAsync(request);
        }
    }
}
