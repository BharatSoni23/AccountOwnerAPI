using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class OwnerForCreationDto
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(60, ErrorMessage ="Name can't be longer than charactera")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Date of birth is Required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage ="Address is Required")]
        [StringLength(100,ErrorMessage ="Address cannot be longer than 100 character")]
        public string? Address { get; set; }

    }
}
