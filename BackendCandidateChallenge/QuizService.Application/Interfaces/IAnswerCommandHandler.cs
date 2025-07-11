using QuizService.Application.Answer.Commands;

namespace QuizService.Application.Interfaces;

public interface IAnswerCommandHandler
{
    Task<int> HandleAsync(CreateAnswerCommand command);
    Task<bool> HandleAsync(UpdateAnswerCommand command);
    Task<bool> HandleAsync(DeleteAnswerCommand command);
}