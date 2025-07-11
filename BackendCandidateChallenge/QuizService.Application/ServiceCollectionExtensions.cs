using Microsoft.Extensions.DependencyInjection;
using QuizService.Application.Answer.Handlers;
using QuizService.Application.Interfaces;
using QuizService.Application.Question.Handlers;
using QuizService.Application.Quiz.Handlers;

namespace QuizService.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Command Handlers
        services.AddScoped<IQuizCommandHandler, QuizCommandHandler>();
        services.AddScoped<IQuestionCommandHandler, QuestionCommandHandler>();
        services.AddScoped<IAnswerCommandHandler, AnswerCommandHandler>();

        // Query Handlers  
        services.AddScoped<IQuizQueryHandler, QuizQueryHandler>();

        return services;
    }
}