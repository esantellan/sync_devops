
namespace Metrics.Application.Interfaces
{
    public interface IBackgroundTaskHandlerResolver
    {
        IBackgroundTaskHandler Resolve(string type);
    }
}
