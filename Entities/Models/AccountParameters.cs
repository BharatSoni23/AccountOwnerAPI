using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AccountParameters:QueryStringParameters //Inherit QueryStringParameters class
    {
        public AccountParameters()
        {
            OrderBy = "Date Created";
        }

    }
}
