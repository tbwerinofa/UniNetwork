using System;
using System.Collections.Generic;

namespace BusinessObject.ViewModel
{
    public class DashboardViewModel
    {
        public int NewMemberCount { get; set; }

        public int RaceCount { get; set; }

        public int ParticipantCount { get; set; }

        public int MemberCount { get; set; }

        public IEnumerable<CourseHistoryViewModel> CourseHistory { get; set; }

        public IEnumerable<DashboardItem> MemberByAgeGroup { get; set; }

        public IEnumerable<DashboardItem> LatestRaceByDistance{ get; set; }

        public RaceQLViewModel LatestRace { get; set; }
        public IEnumerable<DashboardItem> EmployeePerRegion { get; set; }

        public IEnumerable<DashboardItem> IncidentYTD { get; set; }

        public DashboardItem LatestNewsLetter { get; set; }

        public DashboardItem LatestEvent { get; set; }
    }


    public class DashboardItem
    {

        public int FinYear { get; set; }

        public string Group { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int Ordinal { get; set; }

        public int Count { get; set; }

        public string Type { get; set; }

        public decimal Score { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public string Message { get; set; }

        public string TimeString { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public string FinYearMonth { get; set; }

        public string X { get; set; }

        public int Y { get; set; }
        public decimal Amount { get; set; }
    }

    public class Chart
    {
        public string Category { get; set; }

        public int Series { get; set; }
    }

    public class Coordinates
    {

        public string x { get; set; }

        public int y { get; set; }
    }
}
