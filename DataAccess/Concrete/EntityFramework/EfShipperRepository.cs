using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShipperRepository : EfEntityRepositoryBase<Shipper, NorthwindContext>, IShipperRepository
    {
    }
}
