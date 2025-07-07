﻿using QuizService.Model.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizService.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAsync();
    }
}