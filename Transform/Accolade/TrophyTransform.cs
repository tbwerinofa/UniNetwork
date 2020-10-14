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
    public static class TrophyTransform
    {
        /// <summary>
        /// Convert MNC Object into MNC Entity
        /// </summary>
        ///<param name="model">MNC</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>DataAccess.MNC</returns>
        public static Trophy ToEntity(this TrophyViewModel model,Trophy entity
           )
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.IsActive = model.IsActive;
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.DocumentId = model.DocumentId;

            return entity;
        }

        public static IEnumerable<TrophyViewModel> ToListViewModel(
           this IQueryable<Trophy> entity)
        {
            return entity
                .Include(c => c.Document)
               .Include(c => c.UpdatedUser)
                   .Include(c => c.CreatedUser)
                 .IgnoreQueryFilters()
                .AsNoTracking()
                .ToList().Select(a =>
                        new TrophyViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            DocumentId = a.DocumentId,
                            DocumentName = a.Document!= null? a.Document.Name: string.Empty,
                            Name = a.Name,
                            Description = a.Description,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }

        /// <summary>
        /// Convert MNC Entity  into MNC Object
        /// </summary>
        ///<param name="model">TrophyViewModel</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>TrophyViewModel</returns>
        public static TrophyViewModel ToViewModel(this Trophy entity,
            TrophyViewModel model)
        {
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.DocumentId = entity.DocumentId;
            model.DocumentName = entity.Document != null ? entity.Document.Name : string.Empty;
            model.Name = entity.Name;
            model.Description = entity.Description;
            return model;
        }

        public static TrophyQLViewModel ToQueryViewModel(this Trophy entity,
              TrophyQLViewModel model)
        {
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.DocumentId = entity.DocumentId;
            model.DocumentName = entity.Document != null ? entity.Document.Name : string.Empty;
            model.Name = entity.Name;
            model.Description = entity.Description;
            return model;
        }

    }
}
