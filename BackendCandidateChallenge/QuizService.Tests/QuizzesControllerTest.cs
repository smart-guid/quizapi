using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using QuizClient;
using QuizService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QuizService.Tests;

public class QuizzesControllerTest
{
    const string QuizApiEndPoint = "/api/quizzes/";

    [Fact]
    public async Task PostNewQuizAddsQuiz()
    {
        var quiz = new QuizCreateModel("Test title");
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(quiz));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"),
                content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }
    }

    [Fact]
    public async Task AQuizExistGetReturnsQuiz()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 1;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var quiz = JsonConvert.DeserializeObject<QuizResponseModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(quizId, quiz.Id);
            Assert.Equal("My first quiz", quiz.Title);
        }
    }

    [Fact]
    public async Task AQuizDoesNotExistGetFails()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 999;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]
    public async Task AQuizDoesNotExists_WhenPostingAQuestion_ReturnsNotFound()
    {
        const string QuizApiEndPoint = "/api/quizzes/999/questions";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            var question = new QuestionCreateModel("The answer to everything is what?");
            var content = new StringContent(JsonConvert.SerializeObject(question));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]
    public async Task TakeQuizScoresCorrectly2()
    {     
        var quizTitle = "Name Quiz";

        var questions = new List<string>
        {
            "What is my name?",
            "What is my dog's name?"
        };

        var answers = new List<string>()
        {
            "Chris",
            "Gus",
            "Joe",
            "Shawn"
        };

        using var testHost = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = testHost.CreateClient();

        var quizClient = new QuizClient.QuizClient(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), client);

        //create quiz

        var quiz = new QuizClient.Quiz { Title = quizTitle };
        var quizResponse = await quizClient.PostQuizAsync(quiz, CancellationToken.None);

        Assert.Equal(HttpStatusCode.Created, quizResponse.StatusCode);
        Assert.NotNull(quizResponse.Value);

        var quizId = int.Parse(quizResponse.Value.ToString().Split('/').Last());

        //create questions & answers
        foreach (var question in questions)
        {
            //create question
            var questionResponse = await quizClient.PostQuestionAsync(quizId, new QuizQuestion { Text = question }, CancellationToken.None);

            Assert.Equal(HttpStatusCode.Created, quizResponse.StatusCode);
            Assert.NotNull(questionResponse.Value);

            var questionId = int.Parse(questionResponse.Value.ToString().Split('/').Last());

            for (var i = 0; i < answers.Count; i++)
            {
                var answer = answers[i];

                //create answer
                var answerResponse = await quizClient.PostAnswerAsync(quizId, questionId, new QuizClient.Answer { QuestionId = questionId, Text = answer }, CancellationToken.None);

                Assert.Equal(HttpStatusCode.Created, answerResponse.StatusCode);
                Assert.NotNull(answerResponse.Value);

                var answerId = int.Parse(answerResponse.Value.ToString().Split('/').Last());

                //update question to use the correctAnswerId
                var updateQuestionResponse = await quizClient.PutQuestionAsync(quizId, questionId, new QuizQuestionAnswer { Text = question, CorrectAnswerId = answerId }, CancellationToken.None);

                Assert.Equal(HttpStatusCode.NoContent, updateQuestionResponse.StatusCode);              
            }         
        }

        //get the quiz
        var userQuizResponse = await quizClient.GetQuizAsync(quizId, CancellationToken.None);
        var userQuiz = userQuizResponse.Value;

        int score = 0;

        foreach (var userQuestion in userQuiz.Questions)
        {
            var result = await quizClient.SubmitQuestionAnswer(userQuiz.Id, userQuestion.Id, userQuestion.CorrectAnswerId, CancellationToken.None);
            if (result.Value) score++;
        }

        Assert.NotNull(userQuizResponse.Value);
        Assert.Equal(2, score);
    }
}