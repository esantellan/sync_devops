using System;
using System.Collections.Generic;

namespace Metrics.Domain.Entities
{
    public class Project
    {
        /// <summary>
        /// The unique identifier for the project from Azure DevOps.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL to the project in Azure DevOps.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The state of the project (e.g., "wellFormed", "deleting").
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// When the project was last updated in our database.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
