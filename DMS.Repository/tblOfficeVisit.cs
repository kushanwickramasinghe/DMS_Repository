//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMS.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOfficeVisit
    {
        public tblOfficeVisit()
        {
            this.tblInvoices = new HashSet<tblInvoice>();
            this.tblProcedures = new HashSet<tblProcedure>();
        }
    
        public long VisitId { get; set; }
        public System.DateTime Date { get; set; }
        public string Illness { get; set; }
        public string ToothInvolved { get; set; }
        public decimal TotalCharge { get; set; }
        public long AppoinmentNo { get; set; }
        public long PatientId { get; set; }
        public bool Status { get; set; }
    
        public virtual tblAppoiment tblAppoiment { get; set; }
        public virtual ICollection<tblInvoice> tblInvoices { get; set; }
        public virtual ICollection<tblProcedure> tblProcedures { get; set; }
        public virtual tblPatient tblPatient { get; set; }
    }
}
