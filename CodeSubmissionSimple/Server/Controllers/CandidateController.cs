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
    public class CandidatesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidatesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<CandidatesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                //Expression<Func<candidate, bool>> expression = null
                var Candidates = await _unitOfWork.Candidates.GetAll();
                var results = _mapper.Map<IList<CandidateDto>>(Candidates);
                return Ok(results);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        // GET api/<CandidatesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Getcandidate(int id)
        {
            try
            {
                var candidate = await _unitOfWork.Candidates.Get(q => q.CandidateId == id);
                var result = _mapper.Map<CandidateDto>(candidate);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<CandidatesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Createcandidate([FromBody] CandidateDto candidateDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {


                var candidate = _mapper.Map<Candidate>(candidateDto);
                await _unitOfWork.Candidates.Insert(candidate);
                await _unitOfWork.Save();

                return CreatedAtAction("Getcandidate", new { id = candidate.CandidateId }, candidate);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<CandidatesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatecandidate(int id, [FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalcandidate = await _unitOfWork.Candidates.Get(q => q.CandidateId == id);

                if (originalcandidate == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(candidateDto, originalcandidate);
                _unitOfWork.Candidates.Update(originalcandidate);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<CandidatesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletecandidate(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var candidate = await _unitOfWork.Candidates.Get(q => q.CandidateId == id);

                if (candidate == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Candidates.Delete(id);
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
