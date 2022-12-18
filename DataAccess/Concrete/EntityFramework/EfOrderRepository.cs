using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderRepository : EfEntityRepositoryBase<Order, NorthwindContext>, IOrderRepository
    {
    }
}
