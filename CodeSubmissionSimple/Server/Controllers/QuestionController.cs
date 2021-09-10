﻿using AutoMapper;
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
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<QuestionsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestions()
        {
            try
            {
                //Expression<Func<Question, bool>> expression = null
                var questions = await _unitOfWork.Questions.GetAll();
                var results = _mapper.Map<IList<QuestionDto>>(questions);
                return Ok(results);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        // GET api/<QuestionsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuestion(int id)
        {
            try
            {
                var question = await _unitOfWork.Questions.Get(q => q.QuestionId == id);
                var result = _mapper.Map<QuestionDto>(question);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<QuestionsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            try
            {


                var question = _mapper.Map<Question>(questionDto);
                await _unitOfWork.Questions.Insert(question);
                await _unitOfWork.Save();

                return CreatedAtAction("GetQuestion", new { id = question.QuestionId }, question);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.InnerException);
            }


        }


        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var originalQuestion = await _unitOfWork.Questions.Get(q => q.QuestionId == id);

                if (originalQuestion == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(questionDto, originalQuestion);
                _unitOfWork.Questions.Update(originalQuestion);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = await _unitOfWork.Questions.Get(q => q.QuestionId == id);

                if (question == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Questions.Delete(id);
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
