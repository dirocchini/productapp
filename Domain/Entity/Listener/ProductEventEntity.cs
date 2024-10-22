using Domain.Entity.Database;
using Domain.Enum;

namespace Domain.Entity.Listener
{
    public class ProductEventEntity
    {
        public ProductEntity ProductEntity { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
        public ProductEventEnum EventEnum { get; set; }
    }
}
