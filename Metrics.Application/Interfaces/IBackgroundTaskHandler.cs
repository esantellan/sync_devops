
using System.Threading.Tasks;

namespace Metrics.Application.Interfaces
{
    public interface IBackgroundTaskHandler
    {
        string Type { get; }
        Task Handle(string arguments);
    }
}
