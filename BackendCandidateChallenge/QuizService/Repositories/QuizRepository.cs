using Dapper;
using QuizService.Model.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuizService.Repositories;

public class QuizRepository(IDbConnection connection) : IQuizRepository
{


    public async Task<IEnumerable<Quiz>> GetAsync()
    {
        const string sql = "SELECT * FROM Quiz;";
        var quizzes = await connection.QueryAsync<Quiz>(sql);
        return quizzes.Select(x => x);
    }



}