using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Shared
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Description { get; set; }

        public string Langauge { get; set; }

        public string CodeStub { get; set; }

        public List<TestStatus> Answers { get; set; }
    }
}
