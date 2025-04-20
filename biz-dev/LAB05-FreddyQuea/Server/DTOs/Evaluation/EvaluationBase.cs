namespace Server.DTOs.Evaluation;

public class EvaluationBase
{
    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public decimal? Grade { get; set; }
    public DateOnly? Date { get; set; }
}