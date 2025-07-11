namespace QuizService.Application.Question.Dtos;

public class QuestionUpdateModel
{
    public string Text { get; set; } = string.Empty;
    public int CorrectAnswerId { get; set; }
}