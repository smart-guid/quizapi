namespace QuizService.Application.Quiz.Dtos;

public class QuizResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<QuestionItem> Questions { get; set; } = new List<QuestionItem>();
    public Dictionary<string, string> Links { get; set; } = new();

    public class QuestionItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public IEnumerable<AnswerItem> Answers { get; set; } = new List<AnswerItem>();
        public int? CorrectAnswerId { get; set; }
    }

    public class AnswerItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}