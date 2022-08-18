using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementSystem.Models
{
    public class Bank
    {
        public int Id { get; set; }
        [Required]
        public long AccountNumber { get; set; }
        public enum BankType { Public, Private}
        [Required]
        public BankType Type { get; set; }
    }
}
