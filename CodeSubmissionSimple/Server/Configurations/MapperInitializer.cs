using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSubmissionSimple.Server.Models;
using CodeSubmissionSimple.Shared;

namespace CodeSubmissionSimple.Server.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<TestStatus, TestStatusDto>().ReverseMap();
            CreateMap<Submission, SubmissionDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Candidate, CandidateDto>().ReverseMap();
        }

    }
}
