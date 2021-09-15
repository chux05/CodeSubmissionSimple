using CodeSubmissionSimple.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Client.Services
{
    public interface ISubmisstionDataService
    {
       
        Task<Submission> AddSubmission(Submission Submission);
        Task UpdateSubmission(Submission Submission);
        public Task<Submission> GetSubmissionDetails(int SubmissionId);
    }
}
