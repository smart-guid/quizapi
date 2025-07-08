namespace QuizService.Model;

//TODO:Move models to their own project so QuizClient and QuizService can share the Models
//applied to all models
public class AnswerCreateModel
{
    public AnswerCreateModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}