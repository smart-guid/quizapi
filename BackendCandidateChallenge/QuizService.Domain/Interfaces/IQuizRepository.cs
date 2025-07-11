using QuizService.Domain.Models;

namespace QuizService.Repositories;

public interface IQuizRepository
{
    Task<IEnumerable<Quiz>> GetAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task<int> CreateAsync(Quiz quiz);
    Task<bool> UpdateAsync(Quiz quiz);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
