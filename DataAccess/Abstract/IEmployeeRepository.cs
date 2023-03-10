using Base.DataAccess;
using Base.Entities;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IEmployeeRepository: IEntityRepository<Employee>
    {
    }
}
