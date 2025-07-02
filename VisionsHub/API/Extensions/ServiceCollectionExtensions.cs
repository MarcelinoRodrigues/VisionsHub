using VisionsHub.Aplication.Interfaces;
using VisionsHub.Aplication.Services;
using VisionsHub.Infra.Repository;

namespace VisionsHub.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<BookRepository, BookRepository>();
            services.AddScoped<StudentRepository, StudentRepository>();
            services.AddScoped<LoanRepository, LoanRepository>();

            return services;
        }
    }
}
