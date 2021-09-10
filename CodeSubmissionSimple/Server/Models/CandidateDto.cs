using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Models
{
    public class CandidateDto
    {
        public int CandidateId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int SubmissionId { get; set; }
        public SubmissionDto Submission { get; set; }
    }
}
