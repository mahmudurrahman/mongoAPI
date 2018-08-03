using MongoAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoAPI.Repository
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestion(string id);
    
        Task<IEnumerable<Question>> GetQuestion(string bodyText, DateTime updatedFrom, long headerSizeLimit);


        Task AddQuestion(Question item);

     
        Task<bool> RemoveQuestion(string id);
        Task<bool> UpdateQuestion(string id, string body);
        Task<bool> RemoveAllQuestions();

    }
}
