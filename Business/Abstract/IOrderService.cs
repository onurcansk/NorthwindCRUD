using Base.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IOrderService:IBaseService<Order>
    {
        IDataResult<Order> GetById(int id);
        IResult Delete(int id);
    }
}
