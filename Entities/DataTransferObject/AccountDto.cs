using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreateted { get; set; }
        public string? AccountType { get; set; }
        
    }
}
