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
    
    public partial class siixsem_DjHist_t
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public siixsem_DjHist_t()
        {
            this.siixsem_pcb_count_m_td = new HashSet<siixsem_pcb_count_m_td>();
            this.siixsem_pcb_count_sm_td = new HashSet<siixsem_pcb_count_sm_td>();
            this.siixsem_pcbSerials_td = new HashSet<siixsem_pcbSerials_td>();
            this.siixsem_pcbSerials_Led2_td = new HashSet<siixsem_pcbSerials_Led2_td>();
        }
    
        public int se_id { get; set; }
        public int se_id_model { get; set; }
        public string se_assembly_desc { get; set; }
        public string se_pair_fg_name { get; set; }
        public System.DateTime se_created_dt { get; set; }
        public int se_dj_qty { get; set; }
        public string se_dj_no { get; set; }
        public string se_assembly_name { get; set; }
        public string se_dj_group { get; set; }
        public int se_complete { get; set; }
        public int se_mpl_no { get; set; }
        public int se_smt_read_c { get; set; }
        public int se_mi_read_c { get; set; }
        public int se_fct_read_c { get; set; }
        public int se_packing_c { get; set; }
        public int se_backflush_divider { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siixsem_pcb_count_m_td> siixsem_pcb_count_m_td { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siixsem_pcb_count_sm_td> siixsem_pcb_count_sm_td { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siixsem_pcbSerials_td> siixsem_pcbSerials_td { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siixsem_pcbSerials_Led2_td> siixsem_pcbSerials_Led2_td { get; set; }
    }
}