using QuizService.Application.Answer.Commands;
using QuizService.Application.Quiz.Dtos;
using QuizService.Application.Quiz.Queries;

namespace QuizService.Application.Interfaces;

public interface IQuizQueryHandler
{
    Task<IEnumerable<QuizResponseModel>> HandleAsync();
    Task<QuizResponseModel> HandleAsync(GetQuizByIdQuery query);
    Task<bool> HandleAsync(SubmitAnswerCommand command);
}