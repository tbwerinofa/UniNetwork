using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Helpers;

namespace DataAccess
{
    [Table(nameof(Member), Schema = SchemaName.Worker)]
    public class Member : AuditBase
    {
        public Member()
        {
            this.RaceResults = new HashSet<RaceResult>();
            this.TimeTrialResults = new HashSet<TimeTrialResult>();
            this.Winners = new HashSet<Winner>();
            this.MemberMappings = new HashSet<MemberMapping>();
            this.RelationMemberMappings = new HashSet<MemberMapping>();
            this.Subscriptions = new HashSet<Subscription>();
            this.SubscriptionHistories = new HashSet<SubscriptionHistory>();
            this.TrainingPlanMembers = new HashSet<TrainingPlanMember>();
            this.MemberLicenses = new HashSet<MemberLicense>();
        }

        public int PersonId { get; set; }

        public string MemberNo { get; set; }

        public string EmmergencyContact1 { get; set; }
        public string EmmergencyContactNo1 { get; set; }
        public string EmmergencyContact2 { get; set; }
        public string EmmergencyContactNo2 { get; set; }
        public string MedicalAidName { get; set; }
        public string MedicalAidNumber { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }

        public string WorkTelephone { get; set; }

        public string HomeTelephone { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<RaceResult> RaceResults { get; set; }
        public virtual ICollection<Winner> Winners { get; set; }
        public virtual ICollection<TimeTrialResult> TimeTrialResults { get; set; }
        public virtual ICollection<MemberMapping> MemberMappings { get; set; }
        public virtual ICollection<MemberMapping> RelationMemberMappings { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }

        public virtual ICollection<TrainingPlanMember> TrainingPlanMembers { get; set; }
        public virtual ICollection<MemberLicense> MemberLicenses { get; set; }
    }
}
