namespace BusinessObject
{
    public class RemittanceSummaryListViewModel
    {

        public int FinYear { get; set; }

        public string MNCPlant { get; set; }
        public string MNCCountry { get; set; }

        public string CalendarMonth { get; set; }

        public int MNCPlantId { get; set; }

        public int MNCCountryId { get; set; }

        public int FinYearId { get; set; }

        public int CalendarMonthId { get; set; }
        public int RecordCount { get; set; }

        public string CurrencyAmount { get; set; }

        public string DocumentName { get; set; }

        public string DocumentExtension { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentGuid { get; set; }

        public int DocumentId { get; set; }

        public string Abbreviation { get; set; }

        public string WorkFlowStatus { get; set; }

        public int CalendarMonthOrdinal { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsFailed { get; set; }

        public bool IsRemittanceError { get; set; }
    }
}

