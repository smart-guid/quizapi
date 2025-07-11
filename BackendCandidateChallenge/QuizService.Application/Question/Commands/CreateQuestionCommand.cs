namespace QuizService.Application.Question.Commands;

public class CreateQuestionCommand
{
    public int QuizId { get; set; }
    public string Text { get; set; } = default!;
}