using QuizService.Application.Question.Commands;

namespace QuizService.Application.Interfaces;

public interface IQuestionCommandHandler
{
    Task<int> HandleAsync(CreateQuestionCommand command);
    Task<bool> HandleAsync(UpdateQuestionCommand command);
    Task<bool> HandleAsync(DeleteQuestionCommand command);
}