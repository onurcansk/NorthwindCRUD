using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDetailRepository : EfEntityRepositoryBase<OrderDetail, NorthwindContext>, IOrderDetailRepository
    {
    }
}
