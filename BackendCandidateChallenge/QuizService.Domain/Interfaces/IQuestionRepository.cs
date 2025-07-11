using QuizService.Domain.Models;

namespace QuizService.Domain.Interfaces;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId);
    Task<Question> GetByIdAsync(int questionId);
    Task<int> CreateAsync(Question question);
    Task<bool> UpdateAsync(Question question);
    Task<bool> DeleteAsync(int questionId);
}

