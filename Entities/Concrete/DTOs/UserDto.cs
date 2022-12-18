using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
    public class UserDto:IDto
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
    }
}
