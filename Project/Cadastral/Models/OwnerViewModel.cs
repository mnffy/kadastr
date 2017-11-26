using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastral.Models
{
    public class OwnerViewModel
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Owner => $"{Name} {Surname}";
    }
}