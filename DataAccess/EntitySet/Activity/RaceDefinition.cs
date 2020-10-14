using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceDefinition), Schema = SchemaName.Activity)]
    public partial class RaceDefinition:AuditBase
    {
        public RaceDefinition()
        {
            this.Races = new HashSet<Race>();
            this.TrainingPlanRaceDefinitions = new HashSet<TrainingPlanRaceDefinition>();
        }
    
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public int DiscplineId { get; set; }
        public int RaceTypeId { get; set; }
        public virtual Province Province { get; set; }
        public virtual Discpline Discpline { get; set; }
        public virtual RaceType RaceType { get; set; }

        public virtual ICollection<Race> Races { get; set; }
        public virtual ICollection<TrainingPlanRaceDefinition> TrainingPlanRaceDefinitions { get; set; }
    }
}
