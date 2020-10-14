using BusinessObject;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class AwardTrophyTransform
    {
        /// <summary>
        /// Convert MNC Object into MNC Entity
        /// </summary>
        ///<param name="model">MNC</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>DataAccess.MNC</returns>
        public static AwardTrophy ToEntity(this AwardTrophyViewModel model,AwardTrophy entity
           )
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
            entity.FinYearId = model.FinYearId;
            entity.TrophyId = model.TrophyId;
            entity.AwardId = model.AwardId;
            entity.StartDate = model.StartDate;

            return entity;
        }

        public static IEnumerable<AwardTrophyViewModel> ToListViewModel(
           this IQueryable<AwardTrophy> entity)
        {
            return entity
                .Include(c => c.FinYear)
                .Include(c => c.Trophy)
                .Include(c => c.Award)
                .Include(c => c.Award.Gender)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToList().Select(a =>
                        new AwardTrophyViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            FinYearId = a.FinYearId,
                            FinYear = a.FinYear.Name,
                            Trophy = a.Trophy.Name,
                            Award = a.Award.Name,
                            Gender = a.Award.Gender !=null? a.Award.Gender.Name:null,
                            Ordinal = a.Award.Ordinal,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }

        /// <summary>
        /// Convert MNC Entity  into MNC Object
        /// </summary>
        ///<param name="model">AwardTrophyViewModel</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>AwardTrophyViewModel</returns>
        public static AwardTrophyViewModel ToViewModel(this AwardTrophy entity,
            AwardTrophyViewModel model)
        {
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.FinYearId = entity.FinYearId;
            model.TrophyId = entity.TrophyId;
            model.AwardId = entity.AwardId;
            model.FinYear = entity.FinYear.Name;
            model.Trophy = entity.Trophy.Name;
            model.Award = entity.Award.Name;
            model.StartDate = entity.StartDate;
            return model;
        }


    }
}
