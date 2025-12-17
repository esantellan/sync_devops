using Metrics.Domain.Entities;
using System.Threading.Tasks;

namespace Metrics.Infrastructure.Persistence.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project> GetById(string id);
    }
}
