using Dapper;
using QuizService.Domain.Interfaces;
using System.Data;

namespace QuizService.Data.Repositories;

public class QuestionRepository(IDbConnection connection) : IQuestionRepository
{
    public async Task<IEnumerable<Domain.Models.Question>> GetByQuizIdAsync(int quizId)
    {
        const string sql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
        return await connection.QueryAsync<Domain.Models.Question>(sql, new { QuizId = quizId });
    }

    public async Task<Domain.Models.Question> GetByIdAsync(int questionId)
    {
        const string sql = "SELECT * FROM Question WHERE Id = @Id;";
        return await connection.QueryFirstOrDefaultAsync<Domain.Models.Question>(sql, new { Id = questionId });
    }

    public async Task<int> CreateAsync(Domain.Models.Question question)
    {
        const string sql = "INSERT INTO Question (Text, QuizId) VALUES(@Text, @QuizId); SELECT LAST_INSERT_ROWID();";
        return await connection.ExecuteScalarAsync<int>(sql, question);
    }

    public async Task<bool> UpdateAsync(Domain.Models.Question question)
    {
        const string sql = "UPDATE Question SET Text = @Text, CorrectAnswerId = @CorrectAnswerId WHERE Id = @Id";
        int rowsUpdated = await connection.ExecuteAsync(sql, question);
        return rowsUpdated > 0;
    }

    public async Task<bool> DeleteAsync(int questionId)
    {
        const string sql = "DELETE FROM Question WHERE Id = @Id";
        int rowsDeleted = await connection.ExecuteAsync(sql, new { Id = questionId });
        return rowsDeleted > 0;
    }
}

