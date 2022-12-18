using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDemographicRepository : EfEntityRepositoryBase<CustomerDemographic, NorthwindContext>, ICustomerDemographicRepository
    {
    }
}
