using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSubmissionSimple.Server.Data;
using CodeSubmissionSimple.Server.IRepositories;
using CodeSubmissionSimple.Shared;

namespace CodeSubmissionSimple.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private ISubmissionRepository _submissions;
        private IGenericRepository<Question> _questions;
        private IGenericRepository<Candidate> _candidates;
        private IGenericRepository<TestStatus> _testStatuses;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ISubmissionRepository Submissions => _submissions ??= new SubmissionRepository(_context);

        public IGenericRepository<Question> Questions => _questions ??= new GenericRepository<Question>(_context);

        public IGenericRepository<Candidate> Candidates => _candidates ??= new GenericRepository<Candidate>(_context);

        public IGenericRepository<TestStatus> TestStatuses => _testStatuses ??= new GenericRepository<TestStatus>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
