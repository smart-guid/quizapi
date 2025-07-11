using QuizService.Application.Quiz.Commands;

namespace QuizService.Application.Interfaces;

public interface IQuizCommandHandler
{
    Task<int> HandleAsync(CreateQuizCommand command);
    Task<bool> HandleAsync(UpdateQuizCommand command);
    Task<bool> HandleAsync(DeleteQuizCommand command);
}