using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            
           

            this.CreatedTitles = new HashSet<Title>();
            this.UpdatedTitles = new HashSet<Title>();

            this.CreatedGenders = new HashSet<Gender>();
            this.UpdatedGenders = new HashSet<Gender>();

            
            this.CreatedIDTypes = new HashSet<IDType>();
            this.UpdatedIDTypes = new HashSet<IDType>();
            
            this.CreatedMemberStagings = new HashSet<MemberStaging>();
            this.UpdatedMemberStagings = new HashSet<MemberStaging>();

            
            #region GIS
            this.CreatedGlobalRegions = new HashSet<GlobalRegion>();
            this.UpdatedGlobalRegions = new HashSet<GlobalRegion>();

            this.CreatedCountries = new HashSet<Country>();
            this.UpdatedCountries = new HashSet<Country>();

            this.CreatedCities = new HashSet<City>();
            this.UpdatedCities = new HashSet<City>();

            this.CreatedTowns = new HashSet<Town>();
            this.UpdatedTowns = new HashSet<Town>();

            this.CreatedSuburbs = new HashSet<Suburb>();
            this.UpdatedSuburbs = new HashSet<Suburb>();

            this.CreatedProvinces = new HashSet<Province>();
            this.UpdatedProvinces = new HashSet<Province>();

            #endregion


        }
        //public int? PersonId { get; set; }
        //public string FirstName { get; set; }
        //public string Surname { get; set; }

        //public string ContactNo { get; set; }

        //public string FullName
        //{
        //    get { return FirstName + " " + Surname; }
        //}

             
            #region global fields

            #region GIS
        public virtual ICollection<GlobalRegion> CreatedGlobalRegions { get; set; }
        public virtual ICollection<GlobalRegion> UpdatedGlobalRegions { get; set; }

        public virtual ICollection<Country> CreatedCountries { get; set; }
        public virtual ICollection<Country> UpdatedCountries { get; set; }

        public virtual ICollection<City> CreatedCities { get; set; }
        public virtual ICollection<City> UpdatedCities { get; set; }

        public virtual ICollection<Town> CreatedTowns { get; set; }
        public virtual ICollection<Town> UpdatedTowns { get; set; }

        public virtual ICollection<Suburb> CreatedSuburbs { get; set; }
        public virtual ICollection<Suburb> UpdatedSuburbs { get; set; }

        public virtual ICollection<Province> CreatedProvinces { get; set; }
        public virtual ICollection<Province> UpdatedProvinces { get; set; }

        #endregion

           
        #region Meta Data
        public virtual ICollection<Title> CreatedTitles { get; set; }
        public virtual ICollection<Title> UpdatedTitles { get; set; }
        public virtual ICollection<Gender> CreatedGenders { get; set; }
        public virtual ICollection<Gender> UpdatedGenders { get; set; }
        public virtual ICollection<IDType> CreatedIDTypes { get; set; }
        public virtual ICollection<IDType> UpdatedIDTypes { get; set; }
      
 
                #endregion
       
        #region Worker
        
        public virtual ICollection<MemberStaging> CreatedMemberStagings { get; set; }
        public virtual ICollection<MemberStaging> UpdatedMemberStagings { get; set; }

        #endregion

        #endregion
        
    }
}
