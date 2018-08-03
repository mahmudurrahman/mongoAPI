using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoAPI.Models;
using MongoAPI.Repository;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public SystemController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        #region
        /// <summary>
        /// Call an initialization - api/system/init
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                _questionRepository.RemoveAllQuestions();
             

                _questionRepository.AddQuestion(new Question()
                {
                    Id = "1",
                    Body = "Test note 1",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new QuestionImage
                    {
                        ImageSize = 10,
                        Url = "http://localhost/image1.png",
                        ThumbnailUrl = "http://localhost/image1_small.png"
                    }
                });

                _questionRepository.AddQuestion(new Question()
                {
                    Id = "2",
                    Body = "Test note 2",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new QuestionImage
                    {
                        ImageSize = 13,
                        Url = "http://localhost/image2.png",
                        ThumbnailUrl = "http://localhost/image2_small.png"
                    }
                });

                _questionRepository.AddQuestion(new Question()
                {
                    Id = "3",
                    Body = "Test note 3",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new QuestionImage
                    {
                        ImageSize = 14,
                        Url = "http://localhost/image3.png",
                        ThumbnailUrl = "http://localhost/image3_small.png"
                    }
                });

                _questionRepository.AddQuestion(new Question()
                {
                    Id = "4",
                    Body = "Test note 4",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new QuestionImage
                    {
                        ImageSize = 15,
                        Url = "http://localhost/image4.png",
                        ThumbnailUrl = "http://localhost/image4_small.png"
                    }
                });

                return "Database NotesDb was created, and collection 'Notes' was filled with 4 sample items";
            }

            return "Unknown";
        }
        #endregion
    }
}