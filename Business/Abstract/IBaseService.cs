using Base.Entities;
using Base.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBaseService<T> where T : class,IEntity, new()
    {
        IResult Add(T Entity);
        IResult Update(T Entity);   
        IDataResult<List<T>> GetAll();
    }
}
