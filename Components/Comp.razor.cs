using CodeSubmissionSimple.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class QuestionRowBase : ComponentBase
    {
        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public List<TestStatus> SelectedQuestions { get; set; }

        public bool Toggle { get; set; } = false;
    }
}
