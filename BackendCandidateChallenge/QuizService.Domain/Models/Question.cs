namespace QuizService.Domain.Models;

public class Question
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Text { get; set; } = default!;
    public int CorrectAnswerId { get; set; }
}