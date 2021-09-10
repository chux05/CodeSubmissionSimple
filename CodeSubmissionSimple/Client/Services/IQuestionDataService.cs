using CodeSubmissionSimple.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Client.Services
{
    interface IQuestionDataService
    {
        Task<List<Question>> GetAllQuestions();
       
    }
}
