using Microsoft.Extensions.Options;
using MongoAPI.Config;
using MongoAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoAPI.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionContext _context = null;

        public QuestionRepository(IOptions<Settings> settings) => _context = new QuestionContext(settings);

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            try
            {
                return await _context.Questions.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Question> GetQuestion(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Questions.Find(Question => Question.Id == id
                                        || Question.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<Question>> GetQuestion(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.Questions.Find(Question => Question.Body.Contains(bodyText) &&
                                       Question.UpdatedOn >= updatedFrom &&
                                       Question.HeaderImage.ImageSize <= headerSizeLimit);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddQuestion(Question item)
        {
            try
            {
                await _context.Questions.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveQuestion(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Questions.DeleteOneAsync(
                        Builders<Question>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateQuestion(string id, string body)
        {
            var filter = Builders<Question>.Filter.Eq(s => s.Id, id);
            var update = Builders<Question>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult
                    = await _context.Questions.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateQuestion(string id, Question item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Questions
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAllQuestions()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Questions.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
