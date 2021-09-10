using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Shared
{
    public class TestStatus
    {
        public int TestStatusId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string Code { get; set; }

        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
