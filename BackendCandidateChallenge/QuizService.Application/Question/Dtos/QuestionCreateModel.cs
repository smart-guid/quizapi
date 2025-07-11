using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService.Application.Question.Dtos;

public class QuestionCreateModel
{
    public string Text { get; set; } = string.Empty;
}