using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Models
{
        [Table("account")]
        public class Account
        {
        //To Map AccounId property to the right column in the database we use Column Attribute inside [].
        //with the Column name.
        [Column("AccountId")]
        
            public Guid Id { get; set; }
            [Required(ErrorMessage = "Date created is required")]
            public DateTime DateCreated { get; set; }
            [Required(ErrorMessage = "Account type is required")]
            public string? AccountType { get; set; }
            [ForeignKey(nameof(Owner))]
            public Guid Ownerid { get; set; }
            public Owner? Owner { get; set; }
        }
    }

