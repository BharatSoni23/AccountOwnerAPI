//using Entities.Models.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Entities.Models;
using Xunit.Sdk;

namespace Entities.Models
{
    [Table("owner")]
    public class Owner
    {
        //To Map AccounId property to the right column in the database we use Column Attribute inside [].
        //with the Column name.
        [Column("Ownerid")]

        public Guid id { get; set; }

        [Required(ErrorMessage="Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 character")]
        public string? Name { get; set; }

        [Required(ErrorMessage="Date of birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage="Address is required")]
        [StringLength(100,ErrorMessage="Address cannot be longer than 100 characters")]
        public string? Address { get; set; }

        public ICollection<Account>? Accounts { get; set; }

    }

}
