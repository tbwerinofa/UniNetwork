using BusinessObject;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class AwardTrophyAuditTransform
    {
        /// <summary>
        /// Convert AwardTrophy Object into AwardTrophy Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.AwardTrophy</param>
        ///<returns>IEnumerable AwardTrophy</returns>
        ///
        public static IEnumerable<AwardTrophyAuditViewModel> ToListViewModel(
            this IQueryable<DataAccess.AwardTrophyAudit> entity)
        {
            return entity
            .Include(a => a.FinYear)
            .Include(a=> a.AwardTrophy.Award)
            .Include(a => a.AwardTrophy.Trophy)
            .ToList()
            .Select(a =>
                        new AwardTrophyAuditViewModel
                        {
                            Id = a.Id,
                            AwardTrophyId = a.AwardTrophyId,
                            FinYearId = a.FinYearId,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            Award = a.AwardTrophy.Award.Name,
                            Trophy = a.AwardTrophy.Trophy.Name
                        });
        }

        /// <summary>
        /// Convert AwardTrophyAudit Object into AwardTrophyAudit Entity
        /// </summary>
        ///<param name="model">AwardTrophyAudit</param>
        ///<param name="AwardTrophyAuditEntity">DataAccess.AwardTrophyAudit</param>
        ///<returns>DataAccess.AwardTrophyAudit</returns>
        public static DataAccess.AwardTrophyAudit ToAwardTrophyAuditEntity(this DataAccess.AwardTrophyAudit entity,
            AwardTrophyAuditViewModel model)
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

            entity.AwardTrophyId = model.AwardTrophyId;
            entity.FinYearId = model.FinYearId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;

            return entity;
        }

        /// <summary>
        /// Convert AwardTrophyAudit Entity  into AwardTrophyAudit Object
        /// </summary>
        ///<param name="model">AwardTrophyAuditViewModel</param>
        ///<param name="AwardTrophyAuditEntity">DataAccess.AwardTrophyAudit</param>
        ///<returns>AwardTrophyAuditViewModel</returns>
        public static AwardTrophyAuditViewModel ToAwardTrophyAuditViewModel(
         this DataAccess.AwardTrophyAudit entity,
         AwardTrophyAuditViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.AwardTrophyId = entity.AwardTrophyId;
            model.FinYearId = entity.FinYearId;
            model.StartDate = entity.StartDate;
            model.EndDate = model.EndDate;
            return model;
        }


        public static DataAccess.AwardTrophyAudit ToAuditEntity(
            this DataAccess.AwardTrophy entity)
        {
            return new DataAccess.AwardTrophyAudit {

                CreatedUserId = entity.CreatedUserId,
                AwardTrophyId = entity.Id,
                FinYearId = entity.FinYearId,
                StartDate = entity.StartDate,
                IsActive = true
        };
        }


    }
}
