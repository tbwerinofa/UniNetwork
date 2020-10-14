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
    public static class AwardTransform
    {
        /// <summary>
        /// Convert MNC Object into MNC Entity
        /// </summary>
        ///<param name="model">MNC</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>DataAccess.MNC</returns>
        public static Award ToEntity(this AwardViewModel model,Award entity
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
            entity.Name = model.Name;
            entity.FrequencyId = model.FrequencyId;
            entity.HasTrophy = model.HasTrophy;
            entity.GenderId = model.GenderId == 0?null: model.GenderId;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        public static IEnumerable<AwardViewModel> ToListViewModel(
           this IQueryable<Award> entity)
        {
            return entity
                .Include(c => c.Frequency)
                .Include(c => c.Gender)
               .Include(c => c.UpdatedUser)
                   .Include(c => c.CreatedUser)
                 .IgnoreQueryFilters()
                .AsNoTracking()
                .ToList().Select(a =>
                        new AwardViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            FrequencyId = a.FrequencyId,
                            Frequency = a.Frequency.Name,
                            Name = a.Name,
                            HasTrophy = a.HasTrophy,
                            Ordinal = a.Ordinal,
                            Gender = a.Gender != null ? a.Gender.Name : string.Empty,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }

        /// <summary>
        /// Convert MNC Entity  into MNC Object
        /// </summary>
        ///<param name="model">AwardViewModel</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>AwardViewModel</returns>
        public static AwardViewModel ToViewModel(this Award entity,
            AwardViewModel model)
        {
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.Name = entity.Name;
            model.Frequency = entity.Frequency.Name;
            model.HasTrophy = entity.HasTrophy;
            model.GenderId = entity.GenderId;
            model.Ordinal = entity.Ordinal;
            model.FrequencyId = entity.FrequencyId;
            model.Gender = entity.Gender != null ? entity.Gender.Name: string.Empty;
            return model;
        }


    }
}
