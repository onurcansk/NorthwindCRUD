using Base.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryRepository: IEntityRepository<Category>
    {
    }
}
