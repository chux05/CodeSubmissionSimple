using CodeSubmissionSimple.Client.Services;
using CodeSubmissionSimple.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeQuestionSimple.Client.Services
{
    public class QuestionDataService : IQuestionDataService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public QuestionDataService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<Question>> GetAllQuestions()
        {
            var response = await _client.GetAsync("api/Question");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var questions = JsonSerializer.Deserialize<List<Question>>(content, _options);
            return questions;
        }
    
    }
}
