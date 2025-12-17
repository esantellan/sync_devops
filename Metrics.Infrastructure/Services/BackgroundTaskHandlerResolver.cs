
using Metrics.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metrics.Infrastructure.Services
{
    public class BackgroundTaskHandlerResolver : IBackgroundTaskHandlerResolver
    {
        private readonly IEnumerable<IBackgroundTaskHandler> _handlers;

        public BackgroundTaskHandlerResolver(IEnumerable<IBackgroundTaskHandler> handlers)
        {
            _handlers = handlers;
        }

        public IBackgroundTaskHandler Resolve(string type)
        {
            var handler = _handlers.FirstOrDefault(h => h.Type == type);

            if (handler == null)
            {
                throw new InvalidOperationException($"No background task handler found for type: {type}");
            }

            return handler;
        }
    }
}
