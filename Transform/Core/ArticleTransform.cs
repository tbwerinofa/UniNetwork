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
    public static class ArticleTransform
    {
        /// <summary>
        /// Convert Article Object into Article Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Article</param>
        ///<returns>IEnumerable Article</returns>
        ///
        public static IEnumerable<ArticleViewModel> ToListViewModel(
            this IQueryable<DataAccess.Article> entity)
        {
            return entity
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new ArticleViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Author = a.Author,
                            Body = a.Body,
                            FinYear = a.FinYear.Name,
                            CalendarMonth = a.CalendarMonth.Name,
                            CalendarMonthOrdinal = a.CalendarMonth.Ordinal,
                            FinYearId = a.FinYearId,
                            IssueNo = a.Newsletters.FirstOrDefault().IssueNo,
                            CalendarMonthId = a.CalendarMonthId,
                            SessionUserId = a.CreatedUserId,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Article Object into Article Entity
        /// </summary>
        ///<param name="model">Article</param>
        ///<param name="ArticleEntity">DataAccess.Article</param>
        ///<returns>DataAccess.Article</returns>
        public static Article ToEntity(this ArticleViewModel model,
            Article entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;
            entity.Author = model.Author;
            entity.Body = model.Body;
            entity.FinYearId = model.FinYearId;
            entity.PublishDate = model.PublishDate;
            entity.CalendarMonthId = model.CalendarMonthId;

            return entity;
        }

        /// <summary>
        /// Convert Article Entity  into Article Object
        /// </summary>
        ///<param name="model">ArticleViewModel</param>
        ///<param name="ArticleEntity">DataAccess.Article</param>
        ///<returns>ArticleViewModel</returns>
        public static ArticleViewModel ToViewModel(
         this DataAccess.Article entity,
         ArticleViewModel model)
        {

            model.Name = entity.Name;
            model.Author = entity.Author;
            model.Body = entity.Body;
            model.Body = entity.Body;
            model.FinYearId = entity.FinYearId;
            model.FinYear = entity.FinYear.Name;
            model.CalendarMonthId = entity.CalendarMonthId;
           model.PublishDate = entity.PublishDate;
            if(entity.Newsletters.Any())
            {
                model.IssueNo = entity.Newsletters.First().IssueNo;
            }
            return model;
        }

        /// <summary>
        /// Convert Article Entity  into Article Object
        /// </summary>
        ///<param name="model">ArticleViewModel</param>
        ///<param name="ArticleEntity">DataAccess.Article</param>
        ///<returns>ArticleViewModel</returns>
        public static ArticleQLViewModel ToQueryViewModel(
         this DataAccess.Article entity,
         ArticleQLViewModel model)
        {

            model.Name = entity.Name;
            model.Author = entity.Author;
            model.Body = entity.Body;
            model.Body = entity.Body;
            model.FinYearId = entity.FinYearId;
            model.FinYear = entity.FinYear.Name;
            model.CalendarMonthId = entity.CalendarMonthId;
            model.CalendarMonth = entity.CalendarMonth.Name;
           model.IssueNo = entity.Newsletters.Any() ? entity.Newsletters.FirstOrDefault().IssueNo:0;
            return model;
        }

        public static DashboardItem ToDashBoardItem(
      this DataAccess.Article entity)
        {
            return new DashboardItem
            {
                Url = "/Article/Detail/" + entity.Id,
             Icon = "fa-newspaper-o",
             Name = "Title: " + entity.Name,
             Ordinal = entity.Newsletters.Any()?entity.Newsletters.FirstOrDefault().IssueNo:0,
             Message = entity.CalendarMonth.Name + " " + entity.FinYear.Name,
             DateTimeStamp = entity.PublishDate
         };
        }
    }
}
