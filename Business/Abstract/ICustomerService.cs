using Base.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICustomerService:IBaseService<Customer>
    {
        IDataResult<Customer> GetById(string id);
        IResult Delete(string id);
    }
}
