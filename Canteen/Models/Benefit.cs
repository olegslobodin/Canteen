//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Canteen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Benefit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Benefit()
        {
            this.Students_Benefits = new HashSet<Students_Benefits>();
        }
    
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Discount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Students_Benefits> Students_Benefits { get; set; }
    }
}
