using Microsoft.Extensions.DependencyInjection;
using QuizService.Data.Repositories;
using QuizService.Domain.Interfaces;
using QuizService.Repositories;

namespace QuizService.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataInfrastructureServices( this IServiceCollection services)
    {        
        // Repositories
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        return services;
    }
}