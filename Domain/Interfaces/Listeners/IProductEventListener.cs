using Domain.Entity.Listener;

namespace Domain.Interfaces.Listeners
{
    public interface IProductEventListener
    {
        Task NewEvent(ProductEventEntity productEventEntity);
    }
}
