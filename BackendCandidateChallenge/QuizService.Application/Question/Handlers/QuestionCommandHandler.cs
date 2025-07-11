using QuizService.Application.Interfaces;
using QuizService.Application.Question.Commands;
using QuizService.Domain.Interfaces;
using QuizService.Repositories;
namespace QuizService.Application.Question.Handlers;


public class QuestionCommandHandler(IQuestionRepository questionRepository, IQuizRepository quizRepository) : IQuestionCommandHandler
{

    public async Task<int> HandleAsync(CreateQuestionCommand command)
    {
        var quiz = await quizRepository.GetByIdAsync(command.QuizId);

        if (quiz == null)
        {
            return -1;
        }

        var question = new Domain.Models.Question
        {
            Text = command.Text,
            QuizId = command.QuizId
        };
        return await questionRepository.CreateAsync(question);
    }

    public async Task<bool> HandleAsync(UpdateQuestionCommand command)
    {
        var question = new Domain.Models.Question
        {
            Id = command.QuestionId,
            Text = command.Text,
            CorrectAnswerId = command.CorrectAnswerId,
            QuizId = command.QuizId
        };
        return await questionRepository.UpdateAsync(question);
    }

    public async Task<bool> HandleAsync(DeleteQuestionCommand command)
    {
        return await questionRepository.DeleteAsync(command.QuestionId);
    }
}