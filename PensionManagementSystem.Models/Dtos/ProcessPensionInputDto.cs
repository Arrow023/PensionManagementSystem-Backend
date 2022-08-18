using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementSystem.Dtos
{
    public class ProcessPensionInputDto
    {
        [Required]
        public string AadhaarNumber { get; set; }
    }
}
