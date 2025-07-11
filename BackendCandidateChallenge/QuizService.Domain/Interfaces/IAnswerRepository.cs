
using QuizService.Domain.Models;

namespace QuizService.Domain.Interfaces;

public interface IAnswerRepository
{
    Task<IEnumerable<Answer>> GetByQuizIdAsync(int quizId);
    Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId);
    Task<Answer> GetByIdAsync(int answerId);
    Task<int> CreateAsync(Answer answer);
    Task<bool> UpdateAsync(Answer answer);
    Task<bool> DeleteAsync(int answerId);
}

