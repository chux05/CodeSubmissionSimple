using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSubmissionSimple.Shared;

namespace CodeSubmissionSimple.Server.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {

        ISubmissionRepository Submissions { get; }

        IGenericRepository<Question> Questions { get; }

        IGenericRepository<Candidate> Candidates { get; }

        IGenericRepository<TestStatus> TestStatuses { get; }

        Task Save();

    }
}
