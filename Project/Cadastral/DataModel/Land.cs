//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadastral.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Land
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Land()
        {
            this.LicenseRequests = new HashSet<LicenseRequest>();
        }
    
        public int LandId { get; set; }
        public int LandTypeId { get; set; }
        public int OwnerId { get; set; }
        public decimal Cost { get; set; }
        public decimal Area { get; set; }
        public string Address { get; set; }
        public int CadastrId { get; set; }
    
        public virtual Cadastr Cadastr { get; set; }
        public virtual LandType LandType { get; set; }
        public virtual Owner Owner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LicenseRequest> LicenseRequests { get; set; }
    }
}
