namespace QuizService.Application.Question.Commands;

public class UpdateQuestionCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = default!;
    public int CorrectAnswerId { get; set; }
}
