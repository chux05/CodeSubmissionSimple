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
        public SubmissionDataService(HttpClient client)
        {
            _httpClient = client;
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

        public Task<Submission> GetSubmissionDetails(int SubmissionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSubmission(Submission Submission)
        {
            throw new NotImplementedException();
        }
    }
}
