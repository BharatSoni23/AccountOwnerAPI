using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class OwnerDto
    {
        public Guid id { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }   
        public string? Address { get; set; }

        public IEnumerable<AccountDto>? Accounts { get; set; }

    }
}
