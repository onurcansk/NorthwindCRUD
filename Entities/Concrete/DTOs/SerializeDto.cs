using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
    public class SerializeDto:IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Auth { get; set; }
        public int Id { get; set; }
    }
}
