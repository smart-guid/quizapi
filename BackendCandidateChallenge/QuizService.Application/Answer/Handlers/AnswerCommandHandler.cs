using QuizService.Application.Answer.Commands;
using QuizService.Application.Interfaces;
using QuizService.Domain.Interfaces;

namespace QuizService.Application.Answer.Handlers;

public class AnswerCommandHandler(IAnswerRepository answerRepository) : IAnswerCommandHandler
{
    public async Task<int> HandleAsync(CreateAnswerCommand command)
    {
        var answer = new Domain.Models.Answer
        {
            Text = command.Text,
            QuestionId = command.QuestionId
        };
        return await answerRepository.CreateAsync(answer);
    }

    public async Task<bool> HandleAsync(UpdateAnswerCommand command)
    {
        var answer = new Domain.Models.Answer
        {
            Id = command.AnswerId,
            Text = command.Text,
            QuestionId = command.QuestionId
        };
        return await answerRepository.UpdateAsync(answer);
    }

    public async Task<bool> HandleAsync(DeleteAnswerCommand command)
    {
        return await answerRepository.DeleteAsync(command.AnswerId);
    }
}