using BusinessObject.Component;
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
    public static class CountryTransform
    {
        /// <summary>
        /// Convert Country Object into Country Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Country</param>
        ///<returns>IEnumerable Country</returns>
        ///
        public static IEnumerable<CountryViewModel> ToListViewModel(
            this IQueryable<DataAccess.Country> entity)
        {
            return entity
                .Include(c => c.GlobalRegion)
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new CountryViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            GlobalRegionId = a.GlobalRegionId,
                            GlobalRegion = a.GlobalRegion.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Country Object into Country Entity
        /// </summary>
        ///<param name="model">Country</param>
        ///<param name="CountryEntity">DataAccess.Country</param>
        ///<returns>DataAccess.Country</returns>
        public static Country ToEntity(this CountryViewModel model,
            Country entity)
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
            entity.GlobalRegionId = model.GlobalRegionId;
            entity.Name = model.Name;
            return entity;
        }

        /// <summary>
        /// Convert Country Entity  into Country Object
        /// </summary>
        ///<param name="model">CountryViewModel</param>
        ///<param name="CountryEntity">DataAccess.Country</param>
        ///<returns>CountryViewModel</returns>
        public static CountryViewModel ToViewModel(
         this DataAccess.Country entity,
         CountryViewModel model)
        {

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.GlobalRegionId = entity.GlobalRegionId;
            model.GlobalRegion = entity.GlobalRegion.Name;
            model.IsActive = entity.IsActive;
            return model;
        }


        public static IEnumerable<DropDownListItems> ToDropDownListItem(
         this IQueryable<Country> entityList,
          IEnumerable<int> selectedIds)
        {
            return entityList.Include(a => a.GlobalRegion).Select(a => new DropDownListItems
            {
                Value = a.Id,
                Text = a.Name,
                Value2 = a.GlobalRegion.Name,
              //  Selected = a..Any(b => selectedIds.Contains(b.CorporateUnitId))
            })
            .OrderBy(a => a.Value2)
            .ThenBy(a => a.Text);
        }
    }
}
