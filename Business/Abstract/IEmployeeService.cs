using Base.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        IDataResult<Employee> GetById(int id);
        IDataResult<Employee> Login(UserDto userDto);
        IResult Delete(int id);
    }
}
