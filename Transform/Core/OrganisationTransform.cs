using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class OrganisationTransform
    {

        /// <summary>
        /// Convert Organisation Object into Organisation Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Organisation</param>
        ///<returns>IEnumerable Organisation</returns>
        ///
        public static IEnumerable<OrganisationViewModel> ToListViewModel(
            this IQueryable<DataAccess.Organisation> entity)
        {
            return entity
                 .Include(c => c.OrganisationType)
                  .Include(c => c.Province)
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new OrganisationViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            OrganisationTypeId = a.OrganisationTypeId,
                            Abbreviation = a.Abbreviation,
                            ProvinceId = a.ProvinceId,
                            OrganisationType = a.OrganisationType.Name,
                            Province = a.Province.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Organisation Object into Organisation Entity
        /// </summary>
        ///<param name="model">Organisation</param>
        ///<param name="OrganisationEntity">DataAccess.Organisation</param>
        ///<returns>DataAccess.Organisation</returns>
        public static DataAccess.Organisation ToEntity(this OrganisationViewModel model,
             DataAccess.Organisation entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
  
            }
            entity.Name = model.Name;
            entity.Abbreviation = model.Abbreviation;
            entity.ProvinceId = model.ProvinceId;
            entity.OrganisationTypeId = model.OrganisationTypeId;
           
            return entity;
        }

        /// <summary>
        /// Convert Organisation Entity  into Organisation Object
        /// </summary>
        ///<param name="model">OrganisationViewModel</param>
        ///<param name="OrganisationEntity">DataAccess.Organisation</param>
        ///<returns>OrganisationViewModel</returns>
        public static OrganisationViewModel ToViewModel(
         this DataAccess.Organisation entity,
         OrganisationViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.ProvinceId = entity.ProvinceId;
            model.CountryId = entity.Province.CountryId;
            model.Abbreviation = entity.Abbreviation;
            model.OrganisationTypeId = entity.OrganisationTypeId;
            model.Province = entity.Province.Name;
            model.Country = entity.Province.Country.Name;
            model.OrganisationType = entity.OrganisationType.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
