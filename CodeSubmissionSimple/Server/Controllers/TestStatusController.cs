using AutoMapper;
using CodeSubmissionSimple.Server.IRepositories;
using CodeSubmissionSimple.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSubmissionSimple.Shared;

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
        public async Task<IActionResult> GetTestQuestions()
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
        // GET api/<TestStatussController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTestQuestion(int id)
        {
            try
            {
                var TestStatus = await _unitOfWork.TestStatuses.Get(q => q.TestStatusId == id);
                var result = _mapper.Map<TestStatusDto>(TestStatus);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<TestStatussController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTestStatus([FromBody] TestStatusDto TestStatusDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {
                var TestStatus = _mapper.Map<TestStatus>(TestStatusDto);
                await _unitOfWork.TestStatuses.Insert(TestStatus);
                await _unitOfWork.Save();

                return CreatedAtAction("GetTestQuestion", new { id = TestStatus.TestStatusId }, TestStatus);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<TestStatussController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestStatus(int id, [FromBody] TestStatusDto TestStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalTestStatus = await _unitOfWork.TestStatuses.Get(q => q.TestStatusId == id);

                if (originalTestStatus == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(TestStatusDto, originalTestStatus);
                _unitOfWork.TestStatuses.Update(originalTestStatus);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
