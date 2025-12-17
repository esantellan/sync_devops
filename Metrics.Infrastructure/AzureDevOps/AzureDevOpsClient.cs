using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Metrics.Infrastructure.AzureDevOps.Interfaces;
using Metrics.Infrastructure.AzureDevOps.Models;
using Microsoft.Extensions.Configuration;

namespace Metrics.Infrastructure.AzureDevOps
{
    public class AzureDevOpsClient : IAzureDevOpsClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _organizationUrl;
        private readonly string _personalAccessToken;

        public AzureDevOpsClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _organizationUrl = _configuration["AzureDevOps:OrganizationUrl"];
            _personalAccessToken = _configuration["AzureDevOps:PersonalAccessToken"];

            if (string.IsNullOrEmpty(_organizationUrl) || string.IsNullOrEmpty(_personalAccessToken))
            {
                throw new InvalidOperationException("Azure DevOps Organization URL or Personal Access Token is not configured.");
            }

            ConfigureHttpClient();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_organizationUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Encode PAT for basic authentication
            var encodedPat = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_personalAccessToken}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedPat);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            var requestUrl = "/_apis/projects?api-version=7.0"; // Using a recent, stable API version

            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var projectResponse = JsonSerializer.Deserialize<ProjectResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return projectResponse?.Value ?? new List<ProjectModel>();
        }
    }
}
