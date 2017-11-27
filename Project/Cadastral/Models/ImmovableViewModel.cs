using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class ImmovableViewModel
    {
        public int ImmovableId { get; set; }
        [Required]
        public string Address { get; set; }
        public ImmovableTypeViewModel ImmovableType { get; set; }
        public OwnerViewModel Onwer { get; set; }
        public CadastrViewModel Cadastr { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}