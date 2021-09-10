using CodeSubmissionSimple.Server.Data;
using CodeSubmissionSimple.Server.IRepositories;
using CodeSubmissionSimple.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Repositories
{
    public class SubmissionRepository : GenericRepository<Submission>, ISubmissionRepository
    {
        
        public SubmissionRepository(ApplicationDbContext context) 
            : base(context)
        {

        }

        public async Task<Submission> GetSubmissionWithQuestions(int id)
        {
            IQueryable<Submission> query = _db;
            //_context.Submissions
            //    .Include(q => q.Answers)
            //    .Include(c => c.Candidate)
            //    .FirstOrDefault(s => s.SubmissionId == id);


            return await query.AsNoTracking().Include(q => q.Answers).ThenInclude(s => s.Question).Include(c => c.Candidate).FirstOrDefaultAsync(s => s.SubmissionId == id);
        }
    }
}
