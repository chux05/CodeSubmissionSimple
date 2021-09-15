using CodeSubmissionSimple.Client.Services;
using CodeSubmissionSimple.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeSubmissionSimple.Client.Services
{
    public class SubmissionDataService : ISubmisstionDataService
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public SubmissionDataService(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<Submission> AddSubmission(Submission Submission)
        {
            var SubmissionJson =
                new StringContent(JsonSerializer.Serialize(Submission), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Submission", SubmissionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Submission>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public Task DeleteSubmission(int SubmissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Submission>> GetAllSubmissions()
        {
            throw new NotImplementedException();
        }

        public async Task<Submission> GetSubmissionDetails(int SubmissionId)
        {
            var response = await _httpClient.GetAsync($"api/Submission/{SubmissionId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var submission = JsonSerializer.Deserialize<Submission>(content, _options);
            return submission;
        }

        public async Task UpdateSubmission(Submission Submission)
        {
            var SubmissionJson =
               new StringContent(JsonSerializer.Serialize(Submission), Encoding.UTF8, "application/json");

             await _httpClient.PutAsync($"api/Submission/{Submission.SubmissionId}", SubmissionJson);

            //if (response.IsSuccessStatusCode)
            //{
            //    return await JsonSerializer.DeserializeAsync<Submission>(await response.Content.ReadAsStreamAsync(),_options);
            //}

            //return null;
        }
    }
}
