using Dapper;
using QuizService.Domain.Interfaces;
using QuizService.Domain.Models;
using System.Data;

namespace QuizService.Data.Repositories;

public class AnswerRepository(IDbConnection connection) : IAnswerRepository
{
    public async Task<IEnumerable<Answer>> GetByQuizIdAsync(int quizId)
    {
        const string sql = @"
            SELECT a.Id, a.Text, a.QuestionId 
            FROM Answer a 
            INNER JOIN Question q ON a.QuestionId = q.Id 
            WHERE q.QuizId = @QuizId;";
        return await connection.QueryAsync<Answer>(sql, new { QuizId = quizId });
    }

    public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
    {
        const string sql = "SELECT * FROM Answer WHERE QuestionId = @QuestionId;";
        return await connection.QueryAsync<Answer>(sql, new { QuestionId = questionId });
    }

    public async Task<Answer> GetByIdAsync(int answerId)
    {
        const string sql = "SELECT * FROM Answer WHERE Id = @Id;";
        return await connection.QueryFirstOrDefaultAsync<Answer>(sql, new { Id = answerId });
    }

    public async Task<int> CreateAsync(Answer answer)
    {
        const string sql = "INSERT INTO Answer (Text, QuestionId) VALUES(@Text, @QuestionId); SELECT LAST_INSERT_ROWID();";
        return await connection.ExecuteScalarAsync<int>(sql, answer);
    }

    public async Task<bool> UpdateAsync(Answer answer)
    {
        const string sql = "UPDATE Answer SET Text = @Text WHERE Id = @Id";
        int rowsUpdated = await connection.ExecuteAsync(sql, answer);
        return rowsUpdated > 0;
    }

    public async Task<bool> DeleteAsync(int answerId)
    {
        const string sql = "DELETE FROM Answer WHERE Id = @Id";
        int rowsDeleted = await connection.ExecuteAsync(sql, new { Id = answerId });
        return rowsDeleted > 0;
    }
}

