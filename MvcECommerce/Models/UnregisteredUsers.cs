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
    
    public partial class UnregisteredUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnregisteredUsers()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int UnregisterredId { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string Zipcode { get; set; }
        public string District { get; set; }
        public string Mobilephone { get; set; }
        public string FixedLine { get; set; }
        public string Fax { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
