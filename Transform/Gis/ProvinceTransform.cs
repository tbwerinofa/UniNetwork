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
    public static class ProvinceTransform
    {
        /// <summary>
        /// Convert Region Object into Region Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Region</param>
        ///<returns>IEnumerable Region</returns>
        ///
        public static IEnumerable<ProvinceViewModel> ToListViewModel(
            this IQueryable<DataAccess.Province> entity)
        {
            return entity
                .Include(c => c.Country)
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new ProvinceViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CountryId = a.CountryId,
                            Country = a.Country.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Region Object into Region Entity
        /// </summary>
        ///<param name="model">Region</param>
        ///<param name="RegionEntity">DataAccess.Region</param>
        ///<returns>DataAccess.Region</returns>
        public static Province ToEntity(this ProvinceViewModel model,
            Province entity)
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
            entity.CountryId = model.CountryId;
            entity.Name = model.Name;
            return entity;
        }

        /// <summary>
        /// Convert Region Entity  into Region Object
        /// </summary>
        ///<param name="model">RegionViewModel</param>
        ///<param name="RegionEntity">DataAccess.Region</param>
        ///<returns>RegionViewModel</returns>
        public static ProvinceViewModel ToViewModel(
         this DataAccess.Province entity,
         ProvinceViewModel model)
        {
            model.Id = entity.Id;
            model.CountryId = entity.CountryId;
            model.Name = entity.Name;
            model.Country = entity.Country.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

        public static IEnumerable<DropDownListItems> ToDropDownListItem(
       this IQueryable<Province> entityList,
        IEnumerable<int> selectedIds)
        {
            return entityList.Include(a => a.Country).Select(a => new DropDownListItems
            {
                Value = a.Id,
                Text = a.Name,
                Value2 = a.Country.Name,
                //  Selected = a..Any(b => selectedIds.Contains(b.CorporateUnitId))
            })
            .OrderBy(a => a.Value2)
            .ThenBy(a => a.Text);
        }

    }
}
