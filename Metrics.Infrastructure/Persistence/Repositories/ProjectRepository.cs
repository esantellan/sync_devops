using Metrics.Domain.Entities;
using Metrics.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Metrics.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<Project> GetById(string id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
