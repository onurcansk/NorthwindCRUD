using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results.Abstract
{

    public interface IDataResult<T> : IResult
    {
        T Result { get; }
    }
}
