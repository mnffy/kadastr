using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class LandViewModel
    {
        public int LandId { get; set; }
        public LandTypeViewModel LandType { get; set; }
        public OwnerViewModel Owner { get; set; }
        public decimal Cost { get; set; }
        public decimal Area { get; set; }
        public string Address { get; set; }
        public CadastrViewModel Cadastr { get; set; }
    }
}