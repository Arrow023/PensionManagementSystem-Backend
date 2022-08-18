using PensionManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementSystem.Dtos
{
    public class PensionerDetailDto
    {
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PAN { get; set; }
        [Required]
        public long AadhaarNumber { get; set; }
        [Required]
        public double SalaryEarned { get; set; }
        [Required]
        public double Allowances { get; set; }
        public enum PensionType { Self, Family }
        [Required]
        public PensionType Type { get; set; }

        public int BankId { get; set; }
        [ForeignKey("BankId")]
        public Bank? Bank { get; set; }
    }
}
