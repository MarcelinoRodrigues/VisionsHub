using VisionsHub.Aplication.DTOs.Request;

namespace VisionsHub.Aplication.Interfaces
{
    public interface IStudentService
    {
        Task Create(StudentRequest request);
    }
}
