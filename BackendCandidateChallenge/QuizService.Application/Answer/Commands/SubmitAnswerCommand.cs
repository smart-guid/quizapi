namespace QuizService.Application.Answer.Commands;

public class SubmitAnswerCommand
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}