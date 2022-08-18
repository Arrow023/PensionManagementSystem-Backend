using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementSystem.Models
{
    public class ProcessPensionInput
    {
        [Required]
        public string AadhaarNumber { get; set; }
    }
}
