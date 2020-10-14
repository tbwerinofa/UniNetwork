using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(FinYear), Schema = SchemaName.Meta)]
    public partial class FinYear : AuditBase
    {
        public FinYear()
        {
            this.AwardTrophyAudits = new HashSet<AwardTrophyAudit>();
            this.AwardTrophies = new HashSet<AwardTrophy>();
            this.Calendars = new HashSet<Calendar>();
            this.Quotes = new HashSet<Quote>();
            this.FinYearCycles = new HashSet<FinYearCycle>();
            this.Races = new HashSet<Race>();
            this.Winners = new HashSet<Winner>();
            this.SystemDocuments = new HashSet<SystemDocument>();
            this.TrainingPlans = new HashSet<TrainingPlan>();
            this.MemberLicenses = new HashSet<MemberLicense>();
        }
        public int Name { get; set; }
        public System.DateTime StartDate { get; set; }

        public System.DateTime EndDate { get; set; }
        public virtual ICollection<FinYearCycle> FinYearCycles { get; set; }

        public virtual ICollection<Winner> Winners { get; set; }

        public virtual ICollection<AwardTrophy> AwardTrophies { get; set; }

        public virtual ICollection<Calendar> Calendars { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<Race> Races { get; set; }
        public virtual ICollection<AwardTrophyAudit> AwardTrophyAudits { get; set; }

        public virtual ICollection<SystemDocument> SystemDocuments { get; set; }

        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
        public virtual ICollection<MemberLicense> MemberLicenses { get; set; }

    }

}