using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class CadastrViewModel
    {
        public int CadastrId { get; set; }
        [Required]
        public string CadastrName { get; set; }
    }
}