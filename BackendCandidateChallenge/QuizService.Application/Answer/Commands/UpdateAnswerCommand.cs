namespace QuizService.Application.Answer.Commands;

public class UpdateAnswerCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public string Text { get; set; } = default!;
}