//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodOrderingBuddy.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotifyMeService
    {
        public int NotifyMeServiceId { get; set; }
        public int OrderId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string NotifiyMeRequest { get; set; }
        public string NotifyMeResponse { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
