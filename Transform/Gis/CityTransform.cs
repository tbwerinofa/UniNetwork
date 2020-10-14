using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class CityTransform
    {
        /// <summary>
        /// Convert City Object into City Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.City</param>
        ///<returns>IEnumerable City</returns>
        ///
        public static IEnumerable<CityViewModel> ToListViewModel(
            this IQueryable<DataAccess.City> entity)
        {
            return entity
                .Include(c => c.Province)
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new CityViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CountryId = a.ProvinceId,
                            Country = a.Province.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert City Object into City Entity
        /// </summary>
        ///<param name="model">City</param>
        ///<param name="CityEntity">DataAccess.City</param>
        ///<returns>DataAccess.City</returns>
        public static City ToEntity(this CityViewModel model,
            City entity)
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
            entity.ProvinceId = model.CountryId;
            entity.Name = model.Name;
            return entity;
        }

        /// <summary>
        /// Convert City Entity  into City Object
        /// </summary>
        ///<param name="model">CityViewModel</param>
        ///<param name="CityEntity">DataAccess.City</param>
        ///<returns>CityViewModel</returns>
        public static CityViewModel ToViewModel(
         this DataAccess.City entity,
         CityViewModel model)
        {
            model.Id = entity.Id;
            model.CountryId = entity.ProvinceId;
            model.Name = entity.Name;
            model.Country = entity.Province.Name;
            model.IsActive = entity.IsActive;
            return model;
        }



    }
}
