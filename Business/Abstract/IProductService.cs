using Base.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService:IBaseService<Product>
    {
        IDataResult<Product> GetById(int id);
        IResult Delete(int id);
    }
}
