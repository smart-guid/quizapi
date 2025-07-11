using QuizService.Application.Answer.Commands;
using QuizService.Application.Interfaces;
using QuizService.Application.Quiz.Dtos;
using QuizService.Application.Quiz.Queries;
using QuizService.Domain.Interfaces;
using QuizService.Repositories;

namespace QuizService.Application.Quiz.Handlers;

public class QuizQueryHandler(
                IQuizRepository quizRepository,
                IQuestionRepository questionRepository,
                IAnswerRepository answerRepository) : IQuizQueryHandler
{
    public async Task<IEnumerable<QuizResponseModel>> HandleAsync()
    {
        var quizzes = await quizRepository.GetAsync();
        return quizzes.Select(quiz => new QuizResponseModel
        {
            Id = quiz.Id,
            Title = quiz.Title
        });
    }

    public async Task<QuizResponseModel> HandleAsync(GetQuizByIdQuery query)
    {
        var quiz = await quizRepository.GetByIdAsync(query.QuizId);
        if (quiz == null)
            return default!;

        var questions = await questionRepository.GetByQuizIdAsync(query.QuizId);
        var answers = await answerRepository.GetByQuizIdAsync(query.QuizId);

        var answersDict = answers
            .GroupBy(a => a.QuestionId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return new QuizResponseModel
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Questions = questions.Select(question => new QuizResponseModel.QuestionItem
            {
                Id = question.Id,
                Text = question.Text,
                Answers = answersDict.ContainsKey(question.Id)
                    ? answersDict[question.Id].Select(answer => new QuizResponseModel.AnswerItem
                    {
                        Id = answer.Id,
                        Text = answer.Text
                    })
                    : new QuizResponseModel.AnswerItem[0],
                CorrectAnswerId = question.CorrectAnswerId
            }),
            Links = new Dictionary<string, string>
            {
                {"self", $"/api/quizzes/{quiz.Id}"},
                {"questions", $"/api/quizzes/{quiz.Id}/questions"}
            }
        };
    }

    public async Task<bool> HandleAsync(SubmitAnswerCommand command)
    {
        var question = await questionRepository.GetByIdAsync(command.QuestionId);
        if (question == null)
            return false;

        return question.CorrectAnswerId == command.AnswerId;
    }
}