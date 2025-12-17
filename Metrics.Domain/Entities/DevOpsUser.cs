using System;

namespace Metrics.Domain.Entities
{
    public class DevOpsUser
    {
        /// <summary>
        /// The unique identifier for the user from Azure DevOps.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The user's unique name (often an email or alias).
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// The URL to the user's avatar image.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The URL to the user's profile.
        /// </summary>
        public string Url { get; set; }
    }
}
