using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Models
{
    public class TestStatusDto
    {
        public int TestStatusId { get; set; }
        public string Code { get; set; }
        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }

        public int SubmissionId { get; set; }
        public SubmissionDto Submission { get; set; }
    }
}
