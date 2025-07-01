namespace VisionsHub.Aplication.DTOs.Response
{
    public class ReportResponse
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string FirstStudentName { get; set; } = string.Empty;
        public string LastStudentName { get; set; } = string.Empty;
        public string TopStudentName { get; set; } = string.Empty;
        public int TotalLoans { get; set; }
    }
}
