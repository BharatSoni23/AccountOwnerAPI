using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OwnerParameters:QueryStringParameters
    {
        public OwnerParameters()
        {
            OrderBy = "name";
        }


        //uint(Unsigned int) to avoid negative year values.
       public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; }

        public bool VaildYearRange => MaxYearOfBirth > MinYearOfBirth;


        
        public string Name { get; set; }
       
    }
}
