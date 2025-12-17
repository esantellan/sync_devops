using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Metrics.Infrastructure.AzureDevOps.Models
{
    /// <summary>
    /// Represents the overall JSON response for a list of projects from Azure DevOps API.
    /// </summary>
    public class ProjectResponse
    {
        [JsonPropertyName("value")]
        public List<ProjectModel> Value { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents a single project returned from the Azure DevOps API.
    /// </summary>
    public class ProjectModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}
