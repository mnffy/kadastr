using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class ImmovableViewModel
    {
        public int ImmovableId { get; set; }
        public string Address { get; set; }
        public ImmovableTypeViewModel ImmovableType { get; set; }
        public OwnerViewModel Onwer { get; set; }
        public CadastrViewModel Cadastr { get; set; }
        public decimal Cost { get; set; }
    }
}