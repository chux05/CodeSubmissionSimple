using CodeSubmissionSimple.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.IRepositories
{
    public interface ISubmissionRepository : IGenericRepository<Submission>
    {
        public Task<Submission> GetSubmissionWithQuestions(int id);
    }
}
