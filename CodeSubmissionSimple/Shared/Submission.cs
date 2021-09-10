using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Shared
{
    public class Submission
    {
        public int SubmissionId { get; set; }

        public bool isCompleted { get; set; }

        public Candidate Candidate { get; set; }

        public List<TestStatus> Answers { get; set; }
    }
}
