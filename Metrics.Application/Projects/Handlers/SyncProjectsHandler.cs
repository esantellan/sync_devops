using MediatR;
using Metrics.Application.Projects.Commands;
using Metrics.Domain.Entities;
using Metrics.Infrastructure.AzureDevOps.Interfaces;
using Metrics.Infrastructure.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Metrics.Application.Projects.Handlers
{
    public class SyncProjectsHandler : IRequestHandler<SyncProjectsCommand, Unit>
    {
        private readonly IAzureDevOpsClient _azureDevOpsClient;
        private readonly IProjectRepository _projectRepository;

        public SyncProjectsHandler(IAzureDevOpsClient azureDevOpsClient, IProjectRepository projectRepository)
        {
            _azureDevOpsClient = azureDevOpsClient;
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(SyncProjectsCommand request, CancellationToken cancellationToken)
        {
            var projects = await _azureDevOpsClient.GetProjects();

            foreach (var project in projects)
            {
                var projectExists = await _projectRepository.GetById(project.Id.ToString());

                if (projectExists == null)
                {
                    await _projectRepository.Add(new Project
                    {
                        Id = project.Id.ToString(),
                        Name = project.Name,
                        Description = project.Description
                    });
                }
            }
            return Unit.Value;
        }
    }
}
