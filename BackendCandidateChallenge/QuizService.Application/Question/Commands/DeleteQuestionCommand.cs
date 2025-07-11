namespace QuizService.Application.Question.Commands;

public class DeleteQuestionCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
}