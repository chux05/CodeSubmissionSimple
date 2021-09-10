using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Models
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }

        public string Description { get; set; }

        public string Langauge { get; set; }

        public string CodeStub { get; set; }

        public List<TestStatusDto> Answers { get; set; }
    }
}
