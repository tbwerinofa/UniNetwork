using DataAccess.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceResultImport), Schema = SchemaName.Activity)]
    public partial class RaceResultImport : AuditBase
    {
        public string RaceDefinition { get; set; }

        public string RaceType { get; set; }
        public string Discpline { get; set; }
        public string Province { get; set; }
        public int FinYear { get; set; }
        public DateTime EventDate { get; set; }
        public string Distance { get; set; }
        public string AgeGroup { get; set; }
        public string Organisation { get; set; }
        public int Position { get; set; }
        public TimeSpan TimeTaken { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        public int Discriminator { get; set; }
        public int? PersonId { get; set; }

        public int? RaceDistanceId { get; set; }

        public virtual Person Person { get; set; }
        public virtual RaceDistance RaceDistance { get; set; }
    }
}