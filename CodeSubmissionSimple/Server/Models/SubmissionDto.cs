using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Models
{
    public class SubmissionDto
    {
        public int SubmissionId { get; set; }

        public bool isCompleted { get; set; }

        public CandidateDto Candidate { get; set; }

        public List<TestStatusDto> Answers { get; set; }
    }
}
