using Dapper;
using System.Data;

namespace QuizService.Repositories;

public class QuizRepository(IDbConnection connection) : IQuizRepository
{
    public async Task<IEnumerable<Domain.Models.Quiz>> GetAsync()
    {
        const string sql = "SELECT * FROM Quiz;";
        var quizzes = await connection.QueryAsync<Domain.Models.Quiz>(sql);
        return quizzes.Select(x => x);
    }

    public async Task<Domain.Models.Quiz> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Quiz WHERE Id = @Id;";
        return await connection.QueryFirstOrDefaultAsync<Domain.Models.Quiz>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Domain.Models.Quiz quiz)
    {
        const string sql = "INSERT INTO Quiz (Title) VALUES(@Title); SELECT LAST_INSERT_ROWID();";
        return await connection.ExecuteScalarAsync<int>(sql, quiz);
    }

    public async Task<bool> UpdateAsync(Domain.Models.Quiz quiz)
    {
        const string sql = "UPDATE Quiz SET Title = @Title WHERE Id = @Id";
        int rowsUpdated = await connection.ExecuteAsync(sql, quiz);
        return rowsUpdated > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Quiz WHERE Id = @Id";
        int rowsDeleted = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsDeleted > 0;
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }
}