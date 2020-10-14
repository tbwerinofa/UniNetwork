using DataAccess.DataMapping;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace DataAccess
{
    public class SqlServerApplicationDbContext : MultiTenantIdentityDbContext
    {
        public SqlServerApplicationDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public SqlServerApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<SqlServerApplicationDbContext> options)
            : base(tenantInfo, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(TenantInfo.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
          .Where(type => !String.IsNullOrEmpty(type.Namespace))
          .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
              type.BaseType.GetGenericTypeDefinition() == typeof(InsightEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
        /*
        #region Meta
        public DbSet<Menu> Menu { get; set; }

        public DbSet<MenuGroup> MenuGroup { get; set; }

        public DbSet<MenuArea> MenuArea { get; set; }

        public DbSet<MenuSection> MenuSection { get; set; }

        public DbSet<Title> Title { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<MaritalStatus> MaritalStatus { get; set; }

        public DbSet<Language> Language { get; set; }

        public DbSet<IDType> IDType { get; set; }

        public DbSet<EmploymentStatus> EmploymentStatus { get; set; }

        public DbSet<Ethnic> Ethnic { get; set; }

        public DbSet<Venue> EducationLevel { get; set; }

        public DbSet<FinYear> FinYear { get; set; }

        public DbSet<CalendarMonth> CalendarMonth { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<Frequency> Frequency { get; set; }

        public DbSet<Venue> Venue { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<DocumentType> DocumentType { get; set; }
        #endregion

        #region Core

        public DbSet<Control> Control { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        */
        public DbSet<MemberStaging> MemberStaging { get; set; }
        /*
        public DbSet<MemberMapping> MemberMapping { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Moderator> Moderator { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<FinYearCycle> FinYearCycle { get; set; }
        public DbSet<Cycle> Cycle { get; set; }

        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<OrganisationType> OrganisationType { get; set; }
        #endregion


        #region Emailer

        public DbSet<EmailAccount> EmailAccount { get; set; }

        public DbSet<MessageTemplate> MessageTemplate { get; set; }

        public DbSet<QueuedEmail> QueuedEmail { get; set; }

        #endregion

        #region Finance
        public DbSet<MemberPosition> MemberPosition { get; set; }
        public DbSet<SubscriptionType> SubscriptionType { get; set; }
        public DbSet<SubscriptionTypeAttribute> SubscriptionTypeAttribute { get; set; }
        public DbSet<SubscriptionTypeRule> SubscriptionTypeRule { get; set; }
        public DbSet<SubscriptionTypeRuleAudit> SubscriptionTypeRuleAudit { get; set; }

        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Quote> Quote { get; set; }

        public DbSet<QuoteDetail> QuoteDetail { get; set; }

        public DbSet<QuoteStatus> QuoteStatus { get; set; }

        public DbSet<PayFastNotify> PayFastNotify { get; set; }
        #endregion

        #region Activity
        public DbSet<AgeGroup> AgeGroup { get; set; }
        public DbSet<Discpline> Discpline { get; set; }
        public DbSet<Distance> Distance { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<RaceResult> RaceResult { get; set; }
        public DbSet<RaceDefinition> RaceDefinition { get; set; }
        public DbSet<RaceDistance> RaceDistance { get; set; }
        public DbSet<RaceResultImport> RaceResultImport { get; set; }
        public DbSet<TimeTrialDistance> TimeTrialDistance { get; set; }

        public DbSet<RaceOrganisation> RaceOrganisation { get; set; }
        public DbSet<RaceType> RaceType { get; set; }
        public DbSet<RunningCategory> RunningCategory { get; set; }
        public DbSet<TimeTrial> TimeTrial { get; set; }
        public DbSet<TimeTrialResult> TimeTrialResult { get; set; }


        public DbSet<TrainingPlan> TrainingPlan { get; set; }

        public DbSet<TrainingPlanDistance> TrainingPlanDistance { get; set; }

        public DbSet<TrainingPlanMember> TrainingPlanMember { get; set; }

        public DbSet<TrainingPlanRaceDefinition> TrainingPlanRaceDefinition { get; set; }
        #endregion
        */
        #region Gis
        public DbSet<Address> Address { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<GlobalRegion> GlobalRegion { get; set; }

        public DbSet<Province> Province { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        #endregion
        /*
        #region Calendar
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Event> Event { get; set; }

        #endregion

        #region Accolade
        public DbSet<Trophy> Trophy { get; set; }
        public DbSet<Award> Award { get; set; }
        public DbSet<AwardTrophy> AwardTrophy { get; set; }
        public DbSet<AwardTrophyAudit> AwardTrophyAudit { get; set; }
        public DbSet<Winner> Winner { get; set; }
        #endregion

        #region Store

        public DbSet<BannerImage> BannerImage { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<StockAlert> StockAlert { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }

        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<FeaturedCategory> FeaturedCategory { get; set; }
        public DbSet<FeaturedImage> FeaturedImage { get; set; }

        public DbSet<Size> Size { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<SortCategory> SortCategory { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<SystemDocument> SystemDocument { get; set; }
        #endregion

        #region result set
        //public DbSet<TrophyWinner_ResultSet> TrophyWinner_ResultSet { get; set; }
        #endregion

        #region Worker

        public DbSet<MemberLicense> MemberLicense { get; set; }

        #endregion

        */
    }
}
