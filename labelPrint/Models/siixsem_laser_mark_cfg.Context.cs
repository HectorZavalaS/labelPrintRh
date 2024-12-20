﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace labelPrint.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class siixsem_laser_mark_cfgEntities : DbContext
    {
        public siixsem_laser_mark_cfgEntities()
            : base("name=siixsem_laser_mark_cfgEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int setConfigToLM(Nullable<int> idlm, string program, string review, string route, string laserPrg, string batcthID, Nullable<int> qtyPanels, Nullable<int> qtyPcbPanel, Nullable<int> hasPanel, Nullable<int> currQtyReleased, Nullable<int> remQtyReleased)
        {
            var idlmParameter = idlm.HasValue ?
                new ObjectParameter("idlm", idlm) :
                new ObjectParameter("idlm", typeof(int));
    
            var programParameter = program != null ?
                new ObjectParameter("program", program) :
                new ObjectParameter("program", typeof(string));
    
            var reviewParameter = review != null ?
                new ObjectParameter("review", review) :
                new ObjectParameter("review", typeof(string));
    
            var routeParameter = route != null ?
                new ObjectParameter("route", route) :
                new ObjectParameter("route", typeof(string));
    
            var laserPrgParameter = laserPrg != null ?
                new ObjectParameter("laserPrg", laserPrg) :
                new ObjectParameter("laserPrg", typeof(string));
    
            var batcthIDParameter = batcthID != null ?
                new ObjectParameter("batcthID", batcthID) :
                new ObjectParameter("batcthID", typeof(string));
    
            var qtyPanelsParameter = qtyPanels.HasValue ?
                new ObjectParameter("qtyPanels", qtyPanels) :
                new ObjectParameter("qtyPanels", typeof(int));
    
            var qtyPcbPanelParameter = qtyPcbPanel.HasValue ?
                new ObjectParameter("qtyPcbPanel", qtyPcbPanel) :
                new ObjectParameter("qtyPcbPanel", typeof(int));
    
            var hasPanelParameter = hasPanel.HasValue ?
                new ObjectParameter("hasPanel", hasPanel) :
                new ObjectParameter("hasPanel", typeof(int));
    
            var currQtyReleasedParameter = currQtyReleased.HasValue ?
                new ObjectParameter("currQtyReleased", currQtyReleased) :
                new ObjectParameter("currQtyReleased", typeof(int));
    
            var remQtyReleasedParameter = remQtyReleased.HasValue ?
                new ObjectParameter("remQtyReleased", remQtyReleased) :
                new ObjectParameter("remQtyReleased", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("setConfigToLM", idlmParameter, programParameter, reviewParameter, routeParameter, laserPrgParameter, batcthIDParameter, qtyPanelsParameter, qtyPcbPanelParameter, hasPanelParameter, currQtyReleasedParameter, remQtyReleasedParameter);
        }
    
        public virtual ObjectResult<getLaserPrg_Result> getLaserPrg(Nullable<int> idModel)
        {
            var idModelParameter = idModel.HasValue ?
                new ObjectParameter("idModel", idModel) :
                new ObjectParameter("idModel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLaserPrg_Result>("getLaserPrg", idModelParameter);
        }
    
        public virtual ObjectResult<getLaserMark_Result> getLaserMark(Nullable<int> idLS)
        {
            var idLSParameter = idLS.HasValue ?
                new ObjectParameter("idLS", idLS) :
                new ObjectParameter("idLS", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLaserMark_Result>("getLaserMark", idLSParameter);
        }
    
        public virtual ObjectResult<getStatus_Result> getStatus(Nullable<int> idLM)
        {
            var idLMParameter = idLM.HasValue ?
                new ObjectParameter("idLM", idLM) :
                new ObjectParameter("idLM", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getStatus_Result>("getStatus", idLMParameter);
        }
    
        public virtual int setTaskToLM(Nullable<int> idlm, string task, string status)
        {
            var idlmParameter = idlm.HasValue ?
                new ObjectParameter("idlm", idlm) :
                new ObjectParameter("idlm", typeof(int));
    
            var taskParameter = task != null ?
                new ObjectParameter("task", task) :
                new ObjectParameter("task", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("setTaskToLM", idlmParameter, taskParameter, statusParameter);
        }
    }
}
