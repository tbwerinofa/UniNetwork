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
    public static class RaceDefinitionTransform
    {

        /// <summary>
        /// Convert RaceDefinition Object into RaceDefinition Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.RaceDefinition</param>
        ///<returns>IEnumerable RaceDefinition</returns>
        ///
        public static IEnumerable<RaceDefinitionViewModel> ToListViewModel(
            this IQueryable<DataAccess.RaceDefinition> entity)
        {
            return entity
                .Include(c => c.Discpline)
                 .Include(c => c.Province.Country)
                .Include(c => c.RaceType)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RaceDefinitionViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Discpline = a.Discpline.Name,
                            RaceType = a.RaceType.Name,
                            Province = a.Province.Name,
                            Country = a.Province.Country.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert RaceDefinition Object into RaceDefinition Entity
        /// </summary>
        ///<param name="model">RaceDefinition</param>
        ///<param name="RaceDefinitionEntity">DataAccess.RaceDefinition</param>
        ///<returns>DataAccess.RaceDefinition</returns>
        public static DataAccess.RaceDefinition ToEntity(this RaceDefinitionViewModel model,
             DataAccess.RaceDefinition entity)
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
            entity.ProvinceId = model.ProvinceId;
            entity.DiscplineId = model.DiscplineId;
            entity.RaceTypeId = model.RaceTypeId;

            return entity;
        }

        /// <summary>
        /// Convert RaceDefinition Entity  into RaceDefinition Object
        /// </summary>
        ///<param name="model">RaceDefinitionViewModel</param>
        ///<param name="RaceDefinitionEntity">DataAccess.RaceDefinition</param>
        ///<returns>RaceDefinitionViewModel</returns>
        public static RaceDefinitionViewModel ToViewModel(
         this DataAccess.RaceDefinition entity,
         RaceDefinitionViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.CountryId = entity.Province.CountryId;
            model.ProvinceId = entity.ProvinceId;
            model.DiscplineId = entity.DiscplineId;
            model.RaceTypeId = entity.RaceTypeId;
            model.IsActive = entity.IsActive;
            model.RaceType = entity.RaceType.Name;
            model.Province = entity.Province.Name;
            model.Country = entity.Province.Country.Name;
            return model;
        }

        public static RaceDefinitionViewModel ToQueryViewModel(
         this DataAccess.RaceDefinition entity,
         RaceDefinitionViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.CountryId = entity.Province.CountryId;
            model.ProvinceId = entity.ProvinceId;
            model.DiscplineId = entity.DiscplineId;
            model.RaceTypeId = entity.RaceTypeId;
            model.IsActive = entity.IsActive;
            model.RaceType = entity.RaceType.Name;
            model.Discpline = entity.Discpline.Name;
            model.Province = entity.Province.Name;
            model.Country = entity.Province.Country.Name;
            return model;
        }

    }

}
