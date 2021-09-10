using CodeSubmissionSimple.Server.Data;
using CodeSubmissionSimple.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Repositories
{
    public class SubmissionRepository : GenericRepository<Submission>
    {
        
        public SubmissionRepository(ApplicationDbContext context) 
            : base(context)
        {

        }

        public Submission GetPatientWithStatusDetails(int id)
        {
            return _context.Submissions
                .Include(q => q.Answers)
                .Include(c => c.Candidate)
                .FirstOrDefault(s => s.SubmissionId == id);
        }
    }
}
