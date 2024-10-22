using Domain.Entity.Database;
using Domain.Entity.Listener;
using Domain.Interfaces.Listeners;

namespace Application.Events;

public class ProductEventPublisher
{
    private readonly IProductEventListener productEventListener;

    public ProductEventPublisher(IProductEventListener productEventListener)
    {
        this.productEventListener = productEventListener;
    }

    public async Task PublishEvent()
    {
        await productEventListener.NewEvent(new ProductEventEntity() { ProductEntity = new ProductEntity("Product X", 1234.2m) });
    }
}

