using QuizService.Application.Interfaces;
using QuizService.Application.Quiz.Commands;
using QuizService.Repositories;

namespace QuizService.Application.Quiz.Handlers;

public class QuizCommandHandler(IQuizRepository quizRepository) : IQuizCommandHandler
{

    public async Task<int> HandleAsync(CreateQuizCommand command)
    {
        var quiz = new Domain.Models.Quiz { Title = command.Title };
        return await quizRepository.CreateAsync(quiz);
    }

    public async Task<bool> HandleAsync(UpdateQuizCommand command)
    {
        var quiz = new Domain.Models.Quiz { Id = command.Id, Title = command.Title };
        return await quizRepository.UpdateAsync(quiz);
    }

    public async Task<bool> HandleAsync(DeleteQuizCommand command)
    {
        return await quizRepository.DeleteAsync(command.Id);
    }
}
