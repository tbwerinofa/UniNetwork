using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SystemDocumentTransform
    {

        /// <summary>
        /// Convert SystemDocument Object into SystemDocument Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.SystemDocument</param>
        ///<returns>IEnumerable SystemDocument</returns>
        ///
        public static IEnumerable<SystemDocumentViewModel> ToListViewModel(
            this IQueryable<DataAccess.SystemDocument> entity)
        {
            return entity.ToList().Select(a =>
                        new SystemDocumentViewModel
                        {
                           Id = a.Id,
                            DocumentName = a.Document.Name,
                            AnchorTagName = a.Document.Name,
                            DocumentNameGuId = a.Document.DocumentNameGuid,
                            Ordinal = a.Ordinal,
                            FinYear = a.FinYear.Name,
                            DocumentType = a.Document.DocumentType.Name,
                           // DocumentPath = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(a.Document.DocumentData)),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                        .OrderBy(e => e.Ordinal)
                        .ThenBy(e => e.DocumentName);
        }

        /// <summary>
        /// Convert SystemDocument Object into SystemDocument Entity
        /// </summary>
        ///<param name="model">SystemDocument</param>
        ///<param name="SystemDocumentEntity">DataAccess.SystemDocument</param>
        ///<returns>DataAccess.SystemDocument</returns>
        public static DataAccess.SystemDocument ToEntity(
            this SystemDocumentViewModel model,
            DataAccess.SystemDocument entity
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
            entity.FinYearId = model.FinYearId;
            

            return entity;
        }

        /// <summary>
        /// Convert SystemDocument Entity  into SystemDocument Object
        /// </summary>
        ///<param name="SystemDocumentEntity">DataAccess.SystemDocument</param>
        ///<returns>SystemDocumentViewModel</returns>
        public static SystemDocumentViewModel ToViewModel(
            this DataAccess.SystemDocument entity, SystemDocumentViewModel model)
        {

                model.SessionUserId = entity.CreatedUserId;
                model.Id = entity.Id;
                model.DocumentId = entity.DocumentId;
                model.DocumentNameGuId = entity.Document.DocumentNameGuid;
                model.DocumentName = entity.Document.Name;
                model.FinYearId = entity.FinYearId;
                model.IsActive = entity.IsActive;
            model.Ordinal = entity.Ordinal;
         return model;

        }

    }
}
