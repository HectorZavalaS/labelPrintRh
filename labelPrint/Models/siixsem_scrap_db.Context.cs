﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class siixsem_scrap_dbEntities : DbContext
    {
        public siixsem_scrap_dbEntities()
            : base("name=siixsem_scrap_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<calculateCostAcum_Result> calculateCostAcum()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<calculateCostAcum_Result>("calculateCostAcum");
        }
    
        public virtual ObjectResult<calculateCostAcumComp_Result> calculateCostAcumComp()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<calculateCostAcumComp_Result>("calculateCostAcumComp");
        }
    
        public virtual ObjectResult<getModFailures_Result> getModFailures()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getModFailures_Result>("getModFailures");
        }
    
        public virtual ObjectResult<Nullable<int>> insert_item(string serial, string djGroup, string assembly, string assemDesc, string we, string bin, Nullable<double> cost, string user, Nullable<int> idDefect, string origin, string model, string loc, string pair_fg)
        {
            var serialParameter = serial != null ?
                new ObjectParameter("serial", serial) :
                new ObjectParameter("serial", typeof(string));
    
            var djGroupParameter = djGroup != null ?
                new ObjectParameter("djGroup", djGroup) :
                new ObjectParameter("djGroup", typeof(string));
    
            var assemblyParameter = assembly != null ?
                new ObjectParameter("assembly", assembly) :
                new ObjectParameter("assembly", typeof(string));
    
            var assemDescParameter = assemDesc != null ?
                new ObjectParameter("assemDesc", assemDesc) :
                new ObjectParameter("assemDesc", typeof(string));
    
            var weParameter = we != null ?
                new ObjectParameter("we", we) :
                new ObjectParameter("we", typeof(string));
    
            var binParameter = bin != null ?
                new ObjectParameter("bin", bin) :
                new ObjectParameter("bin", typeof(string));
    
            var costParameter = cost.HasValue ?
                new ObjectParameter("cost", cost) :
                new ObjectParameter("cost", typeof(double));
    
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            var idDefectParameter = idDefect.HasValue ?
                new ObjectParameter("idDefect", idDefect) :
                new ObjectParameter("idDefect", typeof(int));
    
            var originParameter = origin != null ?
                new ObjectParameter("origin", origin) :
                new ObjectParameter("origin", typeof(string));
    
            var modelParameter = model != null ?
                new ObjectParameter("model", model) :
                new ObjectParameter("model", typeof(string));
    
            var locParameter = loc != null ?
                new ObjectParameter("loc", loc) :
                new ObjectParameter("loc", typeof(string));
    
            var pair_fgParameter = pair_fg != null ?
                new ObjectParameter("pair_fg", pair_fg) :
                new ObjectParameter("pair_fg", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("insert_item", serialParameter, djGroupParameter, assemblyParameter, assemDescParameter, weParameter, binParameter, costParameter, userParameter, idDefectParameter, originParameter, modelParameter, locParameter, pair_fgParameter);
        }
    
        public virtual int insertCompTemp(string compName, Nullable<double> qty, Nullable<double> cost_unity, Nullable<double> cost_tot, Nullable<double> costAcum)
        {
            var compNameParameter = compName != null ?
                new ObjectParameter("compName", compName) :
                new ObjectParameter("compName", typeof(string));
    
            var qtyParameter = qty.HasValue ?
                new ObjectParameter("qty", qty) :
                new ObjectParameter("qty", typeof(double));
    
            var cost_unityParameter = cost_unity.HasValue ?
                new ObjectParameter("cost_unity", cost_unity) :
                new ObjectParameter("cost_unity", typeof(double));
    
            var cost_totParameter = cost_tot.HasValue ?
                new ObjectParameter("cost_tot", cost_tot) :
                new ObjectParameter("cost_tot", typeof(double));
    
            var costAcumParameter = costAcum.HasValue ?
                new ObjectParameter("costAcum", costAcum) :
                new ObjectParameter("costAcum", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertCompTemp", compNameParameter, qtyParameter, cost_unityParameter, cost_totParameter, costAcumParameter);
        }
    
        public virtual int insertItemTemp(Nullable<int> ls, string assemName, string assemDesc, string wp, Nullable<double> cost, Nullable<double> costAcum)
        {
            var lsParameter = ls.HasValue ?
                new ObjectParameter("ls", ls) :
                new ObjectParameter("ls", typeof(int));
    
            var assemNameParameter = assemName != null ?
                new ObjectParameter("assemName", assemName) :
                new ObjectParameter("assemName", typeof(string));
    
            var assemDescParameter = assemDesc != null ?
                new ObjectParameter("assemDesc", assemDesc) :
                new ObjectParameter("assemDesc", typeof(string));
    
            var wpParameter = wp != null ?
                new ObjectParameter("wp", wp) :
                new ObjectParameter("wp", typeof(string));
    
            var costParameter = cost.HasValue ?
                new ObjectParameter("cost", cost) :
                new ObjectParameter("cost", typeof(double));
    
            var costAcumParameter = costAcum.HasValue ?
                new ObjectParameter("costAcum", costAcum) :
                new ObjectParameter("costAcum", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertItemTemp", lsParameter, assemNameParameter, assemDescParameter, wpParameter, costParameter, costAcumParameter);
        }
    
        public virtual int reset_temp_comp()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("reset_temp_comp");
        }
    
        public virtual int reset_temp_or()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("reset_temp_or");
        }
    
        public virtual ObjectResult<Nullable<int>> updateAssemCost(string assemName, Nullable<double> cost)
        {
            var assemNameParameter = assemName != null ?
                new ObjectParameter("assemName", assemName) :
                new ObjectParameter("assemName", typeof(string));
    
            var costParameter = cost.HasValue ?
                new ObjectParameter("cost", cost) :
                new ObjectParameter("cost", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("updateAssemCost", assemNameParameter, costParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> updateCompCost(Nullable<double> id, Nullable<double> qty)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(double));
    
            var qtyParameter = qty.HasValue ?
                new ObjectParameter("qty", qty) :
                new ObjectParameter("qty", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("updateCompCost", idParameter, qtyParameter);
        }
    }
}
