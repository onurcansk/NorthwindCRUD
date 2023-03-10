using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeRepository : EfEntityRepositoryBase<Employee, NorthwindContext>, IEmployeeRepository
    {
    }
}
