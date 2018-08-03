using Microsoft.AspNetCore.Mvc;
using MongoAPI.Models;
using MongoAPI.Models.Request;
using MongoAPI.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IEnumerable<Question>> Get()
        {
            return await _questionRepository.GetAllQuestions();
        }

        [HttpGet("{id}")]
        public async Task<Question> Get(string id)
        {
            return await _questionRepository.GetQuestion(id) ?? new Question();
        }

        /// <summary>
        /// GET api/Questions/text/date/size
        /// ex: http://localhost:53617/api/Questions/Test/2018-01-01/10000
        /// </summary>
        /// <param name="bodyText"></param>
        /// <param name="updatedFrom"></param>
        /// <param name="headerSizeLimit"></param>
        /// <returns></returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Question>> Get(string bodyText,
                                                 DateTime updatedFrom,
                                                 long headerSizeLimit)
        {
            return await _questionRepository.GetQuestion(bodyText, updatedFrom, headerSizeLimit)
                        ?? new List<Question>();
        }

        /// <summary>
        /// POST api/Questions - creates a new Question
        /// </summary>
        /// <param name="newQuestion"></param>
        [HttpPost]
        public void Post([FromBody] QuestionParam newQuestion)
        {
            _questionRepository.AddQuestion(new Question
            {
                Id = newQuestion.Id,
                Body = newQuestion.Body,
                UpdatedOn = DateTime.Now,
                UserId = newQuestion.UserId
            });
        }

        // PUT api/Questions/5 - updates a specific Question
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _questionRepository.UpdateQuestion(id, value);
        }

        // DELETE api/Questions/5 - deletes a specific Question
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var task = _questionRepository.RemoveQuestion(id);
        }
    }
}
