using Domain.Entity.Listener;
using Domain.Interfaces.Listeners;
using Microsoft.Extensions.Logging;

namespace Domain.Infra.Listeners.Listener
{
    public class ProductEventListener : IProductEventListener
    {
        private readonly ILogger<ProductEventListener> logger;

        public ProductEventListener(ILogger<ProductEventListener> logger)
        {
            this.logger = logger;
        }

        public async Task NewEvent(ProductEventEntity productEventEntity)
        {
            logger.LogInformation("ProductEventListener");
        }
    }
}
