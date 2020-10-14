using System;

namespace BusinessObject.ViewModel
{
    public class CourseHistoryViewModel
    {

        public int CapacityBuildingId { get; set; }
        public int CourseId { get; set; }

        public string Course { get; set; }

        public int FinYear { get; set; }

        public DateTime StartDate { get; set; }

        public string StartDateLongDate { get; set; }

        public string EndDateLongDate { get; set; }

        public int ParticipantCount { get; set; }
    }
}
