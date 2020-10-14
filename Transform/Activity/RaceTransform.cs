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
    public static class RaceTransform
    {

        /// <summary>
        /// Convert Race Object into Race Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Race</param>
        ///<returns>IEnumerable Race</returns>
        ///
        public static IEnumerable<RaceViewModel> ToListViewModel(
            this IQueryable<DataAccess.Race> entity)
        {
            return entity
                .Include(c => c.FinYear)
                 .Include(c => c.RaceDefinition.Province.Country)
                .Include(c => c.RaceDefinition.RaceType)
                 .Include(c => c.RaceDistances).ThenInclude(c => c.Distance)
                  .Include(c => c.RaceDistances).ThenInclude(c => c.Race)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RaceViewModel
                        {
                            Id = a.Id,
                            FinYear = a.FinYear.Name,
                            RaceDefinition = a.RaceDefinition.Name,
                            Theme = a.Theme,
                            Province = a.RaceDefinition.Province.Name,
                            Country = a.RaceDefinition.Province.Country.Name,
                            RaceDistances = a.RaceDistances.AsQueryable().ToListViewModel(),
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Race Object into Race Entity
        /// </summary>
        ///<param name="model">Race</param>
        ///<param name="RaceEntity">DataAccess.Race</param>
        ///<returns>DataAccess.Race</returns>
        public static DataAccess.Race ToEntity(this RaceViewModel model,
             DataAccess.Race entity)
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
            entity.Theme = model.Theme;
            entity.RaceDefinitionId = model.RaceDefinitionId;
            entity.FinYearId = model.FinYearId;

            return entity;
        }

        /// <summary>
        /// Convert Race Entity  into Race Object
        /// </summary>
        ///<param name="model">RaceViewModel</param>
        ///<param name="RaceEntity">DataAccess.Race</param>
        ///<returns>RaceViewModel</returns>
        public static RaceViewModel ToViewModel(
         this DataAccess.Race entity,
         RaceViewModel model)
        {
            model.Id = entity.Id;
            model.Theme = entity.Theme;
            model.RaceDefinition = entity.RaceDefinition.Name;
            model.FinYearId = entity.FinYearId;
            model.FinYear = entity.FinYear.Name;
            model.Province = entity.RaceDefinition.Province.Name;
            model.Country = entity.RaceDefinition.Province.Country.Name;
            model.ProvinceId = entity.RaceDefinition.ProvinceId;
            model.CountryId = entity.RaceDefinition.Province.CountryId;
            model.RaceDefinitionId = entity.RaceDefinitionId;

            return model;
        }

        public static RaceQLViewModel ToQueryViewModel(
 this DataAccess.Race entity,
 RaceQLViewModel model)
        {
            model.Id = entity.Id;
            model.Theme = entity.Theme;
            model.RaceDefinition = entity.RaceDefinition.Name;
            model.FinYearId = entity.FinYearId;
            model.FinYear = entity.FinYear.Name;
            model.Province = entity.RaceDefinition.Province.Name;
            model.Country = entity.RaceDefinition.Province.Country.Name;
            model.ProvinceId = entity.RaceDefinition.ProvinceId;
            model.CountryId = entity.RaceDefinition.Province.CountryId;
            model.RaceDefinitionId = entity.RaceDefinitionId;
            model.RaceDistances = entity.RaceDistances.AsQueryable().ToQueryListViewModel();
             model.Participants = entity.RaceDistances.Sum(b => b.RaceResults.Count());
            return model;
        }

        public static IEnumerable<RaceQLViewModel> ToQueryListViewModel(
           this IQueryable<DataAccess.Race> entity)
        {
            return entity
                .Include(c => c.FinYear)
                 .Include(c => c.RaceDefinition.Province.Country)
                .Include(c => c.RaceDefinition.RaceType)
                 .Include(c => c.RaceDistances).ThenInclude(c => c.Distance)
                  .Include(c => c.RaceDistances).ThenInclude(c => c.Race)
                 .Include(c => c.RaceDistances).ThenInclude(c => c.RaceResults).ThenInclude(c => c.Member.Person.Gender)
                  .Include(c => c.RaceDistances).ThenInclude(c => c.RaceResults).ThenInclude(c => c.Member.Person.AgeGroup)
                  .Include(c => c.RaceDistances).ThenInclude(c => c.RaceResults).ThenInclude(c => c.RaceDistance.Distance)
                .AsNoTracking()
                .Where(a=>a.RaceDistances.Any(b=>b.RaceResults.Any()))
                .Select(a =>
                        new RaceQLViewModel
                        {
                            Id = a.Id,
                            FinYear = a.FinYear.Name,
                            RaceDefinition = a.RaceDefinition.Name,
                            Theme = a.Theme,
                            Province = a.RaceDefinition.Province.Name,
                            Country = a.RaceDefinition.Province.Country.Name,
                            RaceDistances = a.RaceDistances.AsQueryable().ToQueryListViewModel(),
                            Participants = a.RaceDistances.Sum(b => b.RaceResults.Count()),
                            EventDateTimes = a.RaceDistances.FirstOrDefault().EventDate,
                            IsActive = a.IsActive,
                        });
        }
    }

}
