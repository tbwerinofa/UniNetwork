using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class DocumentTransform
    {


        public static IEnumerable<DocumentViewModel> ToListViewModel(
          this IQueryable<DataAccess.Document> entity)
        {
            return entity
                .Include(c => c.DocumentType)
                 .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new DocumentViewModel
                        {
                            Id = a.Id,
                            DocumentTypeId = a.DocumentTypeId,
                            DocumentType = a.DocumentType.Name,
                            Name = a.Name,
                            DocumentData = a.DocumentData,
                            Comments = a.Comments,
                            IsActive =a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,
                        });
        }

        /// <summary>
        /// Convert Document Object into Document Entity
        /// </summary>
        ///<param name="model">Document</param>
        ///<param name="DocumentEntity">DataAccess.Document</param>
        ///<returns>DataAccess.Document</returns>
        public static Document ToEntity(this DocumentViewModel model,Document entity
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

            entity.DocumentTypeId = model.DocumentTypeId;
            entity.Name = model.Name;
            entity.DocumentData = model.DocumentData;
            entity.DocumentNameGuid = model.DocumentNameGuid;
            entity.Comments = model.Comments;

            return entity;
        }

        public static DocumentViewModel ToViewModel(this DataAccess.Document entity,
           DocumentViewModel model)
        {
            model.DocumentTypeId = entity.DocumentTypeId;
            model.Name = entity.Name;
            model.DocumentData = entity.DocumentData;
            model.Id = entity.Id;
            model.Comments = entity.Comments;
           
            return model;
        }

    }
}
