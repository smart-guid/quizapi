namespace QuizService.Application.Answer.Commands;

public class CreateAnswerCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = default!;
}