using Microsoft.AspNetCore.Mvc;
using QuizService.Application.Answer.Commands;
using QuizService.Application.Answer.Dtos;
using QuizService.Application.Interfaces;
using QuizService.Application.Question.Commands;
using QuizService.Application.Question.Dtos;
using QuizService.Application.Quiz.Commands;
using QuizService.Application.Quiz.Dtos;
using QuizService.Application.Quiz.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizService.Controllers;

[Route("api/quizzes")]
public class QuizController : Controller
{
    private readonly IQuizCommandHandler _quizCommandHandler;
    private readonly IQuestionCommandHandler _questionCommandHandler;
    private readonly IAnswerCommandHandler _answerCommandHandler;
    private readonly IQuizQueryHandler _quizQueryHandler;

    public QuizController(
        IQuizCommandHandler quizCommandHandler,
        IQuestionCommandHandler questionCommandHandler,
        IAnswerCommandHandler answerCommandHandler,
        IQuizQueryHandler quizQueryHandler)
    {
        _quizCommandHandler = quizCommandHandler;
        _questionCommandHandler = questionCommandHandler;
        _answerCommandHandler = answerCommandHandler;
        _quizQueryHandler = quizQueryHandler;
    }

    // GET api/quizzes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizResponseModel>>> GetAllQuizzes()
    {    
        var result = await _quizQueryHandler.HandleAsync();
        return Ok(result);
    }

    // GET api/quizzes/{quizId}
    [HttpGet("{quizId}")]
    public async Task<ActionResult<QuizResponseModel>> GetQuiz(int quizId)
    {
        var query = new GetQuizByIdQuery { QuizId = quizId };
        var result = await _quizQueryHandler.HandleAsync(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST api/quizzes
    [HttpPost]
    public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateModel model)
    {
        var command = new CreateQuizCommand { Title = model.Title };
        var quizId = await _quizCommandHandler.HandleAsync(command);

        return Created($"/api/quizzes/{quizId}", null);
    }

    // PUT api/quizzes/{quizId}
    [HttpPut("{quizId}")]
    public async Task<IActionResult> UpdateQuiz(int quizId, [FromBody] QuizUpdateModel model)
    {
        var command = new UpdateQuizCommand { Id = quizId, Title = model.Title };
        var success = await _quizCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE api/quizzes/{quizId}
    [HttpDelete("{quizId}")]
    public async Task<IActionResult> DeleteQuiz(int quizId)
    {
        var command = new DeleteQuizCommand { Id = quizId };
        var success = await _quizCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }


    // POST api/quizzes/{quizId}/questions
    [HttpPost("{quizId}/questions")]
    public async Task<IActionResult> CreateQuestion(int quizId, [FromBody] QuestionCreateModel model)
    {
        var command = new CreateQuestionCommand
        {
            QuizId = quizId,
            Text = model.Text
        };
        var questionId = await _questionCommandHandler.HandleAsync(command);

        if (questionId == -1)
        {
            return NotFound();
        }

        return Created($"/api/quizzes/{quizId}/questions/{questionId}", null);
    }

    // PUT api/quizzes/{quizId}/questions/{questionId}
    [HttpPut("{quizId}/questions/{questionId}")]
    public async Task<IActionResult> UpdateQuestion(int quizId, int questionId, [FromBody] QuestionUpdateModel model)
    {
        var command = new UpdateQuestionCommand
        {
            QuizId = quizId,
            QuestionId = questionId,
            Text = model.Text,
            CorrectAnswerId = model.CorrectAnswerId
        };
        var success = await _questionCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }


    // DELETE api/quizzes/{quizId}/questions/{questionId}
    [HttpDelete("{quizId}/questions/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(int quizId, int questionId)
    {
        var command = new DeleteQuestionCommand
        {
            QuizId = quizId,
            QuestionId = questionId
        };
        var success = await _questionCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // POST api/quizzes/{quizId}/questions/{questionId}/answers
    [HttpPost("{quizId}/questions/{questionId}/answers")]
    public async Task<IActionResult> CreateAnswer(int quizId, int questionId, [FromBody] AnswerCreateModel model)
    {
        var command = new CreateAnswerCommand
        {
            QuizId = quizId,
            QuestionId = questionId,
            Text = model.Text
        };
        var answerId = await _answerCommandHandler.HandleAsync(command);

        return Created($"/api/quizzes/{quizId}/questions/{questionId}/answers/{answerId}", null);
    }

    // PUT api/quizzes/{quizId}/questions/{questionId}/answers/{answerId}
    [HttpPut("{quizId}/questions/{questionId}/answers/{answerId}")]
    public async Task<IActionResult> UpdateAnswer(int quizId, int questionId, int answerId, [FromBody] AnswerUpdateModel model)
    {
        var command = new UpdateAnswerCommand
        {
            QuizId = quizId,
            QuestionId = questionId,
            AnswerId = answerId,
            Text = model.Text
        };
        var success = await _answerCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE api/quizzes/{quizId}/questions/{questionId}/answers/{answerId}
    [HttpDelete("{quizId}/questions/{questionId}/answers/{answerId}")]
    public async Task<IActionResult> DeleteAnswer(int quizId, int questionId, int answerId)
    {
        var command = new DeleteAnswerCommand
        {
            QuizId = quizId,
            QuestionId = questionId,
            AnswerId = answerId
        };
        var success = await _answerCommandHandler.HandleAsync(command);

        if (!success)
            return NotFound();

        return NoContent();
    }


    // GET api/quizzes/{quizId}/questions/{questionId}/submit/{answerId}
    [HttpGet("{quizId}/questions/{questionId}/submit/{answerId}")]
    public async Task<ActionResult<bool>> SubmitAnswer(int quizId, int questionId, int answerId)
    {
        var command = new SubmitAnswerCommand
        {
            QuizId = quizId,
            QuestionId = questionId,
            AnswerId = answerId
        };
        var isCorrect = await _quizQueryHandler.HandleAsync(command);

        return Ok(isCorrect);
    }
}