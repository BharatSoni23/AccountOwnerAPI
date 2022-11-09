using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    internal class Link
    {

        public string Herf { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public Link()
        { 
        }

        public Link(string herf, string rel, string method)
        {
            Herf = herf;
            Rel = rel;
            Method = method;
        }
    }
}
