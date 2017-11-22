using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class LicenseRequestModel
    {
        public int LicenseId { get; set; }
        public CadastrViewModel Cadastr { get; set; }
        public string LiicenseRequestState { get; set; }
    }
}