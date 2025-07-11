namespace QuizService.Application.Quiz.Commands;

public class UpdateQuizCommand
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}
