//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcECommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.Products = new HashSet<Products>();
        }
    
        public int OrderId { get; set; }
        public int CouponId { get; set; }
        public int UserId { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<System.DateTime> Registerdate { get; set; }
        public Nullable<double> ShippingPrice { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual CouponCodes CouponCodes { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
