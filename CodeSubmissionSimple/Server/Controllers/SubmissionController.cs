using AutoMapper;
using CodeSubmissionSimple.Server.IRepositories;
using CodeSubmissionSimple.Server.Models;
using CodeSubmissionSimple.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubmissionController(IUnitOfWork unitOfWork, IMapper mapper)
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
                var submissions = await _unitOfWork.Submissions.GetAll();
                var results = _mapper.Map<IList<SubmissionDto>>(submissions);
                return Ok(results);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        // GET api/<SubmissionsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubmission(int id)
        {
            try
            {
                var submission = await _unitOfWork.Submissions.GetSubmissionWithQuestions(id);
                var result = _mapper.Map<SubmissionDto>(submission);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<SubmissionsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSubmission([FromBody] SubmissionDto submissionDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {
                var submission = _mapper.Map<Submission>(submissionDto);
                await _unitOfWork.Submissions.Insert(submission);
                await _unitOfWork.Save();

                return CreatedAtAction("GetSubmission", new { id = submission.SubmissionId }, submission);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<SubmissionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubmission(int id, [FromBody] SubmissionDto submissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalSubmission = await _unitOfWork.Submissions.GetSubmissionWithQuestions(id);

                if (originalSubmission == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

               var submision = _mapper.Map(submissionDto, originalSubmission);
                _unitOfWork.Submissions.Update(submision);

                //bad practice, need to figure to do this in a better way
                for (int i = 0; i < originalSubmission.Answers.Count; i++)
                {
                    _unitOfWork.TestStatuses.Update(submision.Answers[i]);
                }

                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<SubmissionsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubmission(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var submission = await _unitOfWork.Submissions.Get(q => q.SubmissionId == id);

                if (submission == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Submissions.Delete(id);
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