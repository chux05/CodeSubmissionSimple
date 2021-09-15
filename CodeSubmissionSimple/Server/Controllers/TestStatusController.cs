using AutoMapper;
using CodeSubmissionSimple.Server.IRepositories;
using CodeSubmissionSimple.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestStatusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<SubmissionsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubmissions()
        {
            try
            {
                //Expression<Func<Submission, bool>> expression = null
                var testStatuses = await _unitOfWork.TestStatuses.GetAll();
                var results = _mapper.Map<IList<TestStatusDto>>(testStatuses);
                return Ok(results);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
