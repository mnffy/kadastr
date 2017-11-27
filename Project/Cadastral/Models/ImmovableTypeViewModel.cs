using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class ImmovableTypeViewModel
    {
        public int ImmovableTypeId { get; set; }
        [Required]
        public string ImmovableTypeName { get; set; }
    }
}