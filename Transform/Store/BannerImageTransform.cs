using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class BannerImageTransform
    {

        /// <summary>
        /// Convert BannerImage Object into BannerImage Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.BannerImage</param>
        ///<returns>IEnumerable BannerImage</returns>
        ///
        public static IEnumerable<BannerImageViewModel> ToListViewModel(
            this IQueryable<DataAccess.BannerImage> entity)
        {
            return entity.ToList().Select(a =>
                        new BannerImageViewModel
                        {
                           Id = a.Id,
                            DocumentName = a.Document.Name,
                            DocumentNameGuId = a.Document.DocumentNameGuid,
                            Ordinal = a.Ordinal,
                           // DocumentPath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(a.Document.DocumentData)),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                        .OrderBy(e => e.Ordinal)
                        .ThenBy(e => e.DocumentName);
        }

        /// <summary>
        /// Convert BannerImage Object into BannerImage Entity
        /// </summary>
        ///<param name="model">BannerImage</param>
        ///<param name="BannerImageEntity">DataAccess.BannerImage</param>
        ///<returns>DataAccess.BannerImage</returns>
        public static DataAccess.BannerImage ToEntity(
            this BannerImageViewModel model,
            DataAccess.BannerImage entity
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
            entity.DocumentId = model.DocumentId;
            entity.Ordinal = model.Ordinal;

            return entity;
        }

        /// <summary>
        /// Convert BannerImage Entity  into BannerImage Object
        /// </summary>
        ///<param name="BannerImageEntity">DataAccess.BannerImage</param>
        ///<returns>BannerImageViewModel</returns>
        public static BannerImageViewModel ToViewModel(
            this DataAccess.BannerImage entity, BannerImageViewModel model)
        {

                model.SessionUserId = entity.CreatedUserId;
                model.Id = entity.Id;
                model.DocumentId = entity.DocumentId;
                model.DocumentNameGuId = entity.Document.DocumentNameGuid;
                model.DocumentName = entity.Document.Name;
                model.IsActive = entity.IsActive;
            model.Ordinal = entity.Ordinal;
         return model;

        }

    }
}
