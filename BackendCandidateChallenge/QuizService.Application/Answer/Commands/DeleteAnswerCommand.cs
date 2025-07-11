namespace QuizService.Application.Answer.Commands;

public class DeleteAnswerCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}