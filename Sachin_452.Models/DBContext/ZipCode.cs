//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sachin_452.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZipCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZipCode()
        {
            this.UserRideTable = new HashSet<UserRideTable>();
        }
    
        public int ZipCodeId { get; set; }
        public string ZipCodeName { get; set; }
        public Nullable<int> CityId { get; set; }
    
        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRideTable> UserRideTable { get; set; }
    }
}