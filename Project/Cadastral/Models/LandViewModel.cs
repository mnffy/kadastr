using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class LandViewModel
    {
        public int LandId { get; set; }
        public LandTypeViewModel LandType { get; set; }
        public OwnerViewModel Owner { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Cost")]
        public decimal Cost { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Area")]
        public decimal Area { get; set; }
        [Required]
        public string Address { get; set; }
        public CadastrViewModel Cadastr { get; set; }
        public string CurrentUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CurrentUserName => $"{Name} {Surname}";
    }
}