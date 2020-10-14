namespace BusinessObject
{
    public class FeaturedImageViewModel : BaseViewModel
    {
        //public int Id { get; set; }

        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        public int FeaturedCategoryId { get; set; }

        public string Product { get; set; }

        public bool IsCustom { get; set; }

        public decimal? Price { get; set; }

        public string CreatedTimestampFormatted { get; set; }

        public bool IsFeatured { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentGuId { get; set; }

        public int DocumentId { get; set; }

    }
}
