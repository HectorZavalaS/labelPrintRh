//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace labelPrint.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class siixsem_revModel_td
    {
        public int se_id { get; set; }
        public int se_id_review { get; set; }
        public int se_id_model { get; set; }
    
        public virtual siixsem_reviews_t siixsem_reviews_t { get; set; }
    }
}
