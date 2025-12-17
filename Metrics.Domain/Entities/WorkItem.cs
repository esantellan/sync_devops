using System;

namespace Metrics.Domain.Entities
{
    public class WorkItem
    {
        /// <summary>
        /// The unique ID of the work item from Azure DevOps.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The type of the work item (e.g., "Epic", "User Story", "Task", "Bug").
        /// </summary>
        public string WorkItemType { get; set; }

        /// <summary>
        /// The title of the work item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The current state of the work item (e.g., "New", "Active", "Resolved", "Closed").
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The reason for the current state.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The URL to the work item in Azure DevOps.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Foreign key to the Project this work item belongs to.
        /// </summary>
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        /// <summary>
        /// Foreign key to the user this work item is assigned to.
        /// Can be nullable if the work item is unassigned.
        /// </summary>
        public Guid? AssignedToId { get; set; }
        public virtual DevOpsUser AssignedTo { get; set; }

        /// <summary>
        /// When the work item was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// When the work item was last changed.
        /// </summary>
        public DateTime ChangedDate { get; set; }
    }
}
