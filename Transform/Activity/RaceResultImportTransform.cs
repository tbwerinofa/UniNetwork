using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class RaceResultImportTransform
    {

        public static IEnumerable<RaceResultImportViewModel> ToListViewModel(
         this IQueryable<DataAccess.RaceResultImport> entity)
        {
            return entity
                .Include(c => c.Person)
                  .Include(c => c.CreatedUser)
                   .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RaceResultImportViewModel
                        {
                            Id = a.Id,
                            RaceDefinition = a.RaceDefinition,
                            RaceType = a.RaceType,
                            Discpline = a.Discpline,
                            Province = a.Province,
                            FinYear = a.FinYear,
                            EventDate = a.EventDate,
                            Distance = a.Distance,
                            AgeGroup = a.AgeGroup,
                            Organisation = a.Organisation,
                            FirstName = a.FirstName,
                            Surname = a.Surname,
                            FullName = a.FullName,
                            Discriminator = a.Discriminator,
                            RaceDistanceId = a.RaceDistanceId,
                            IsActive = a.IsActive,
                        })
                        .GroupBy(a => new { a.RaceDefinition, a.RaceType, a.Discpline, a.FinYear, a.EventDate, a.Distance, a.RaceDistanceId })
                          .Select(a =>
                        new RaceResultImportViewModel
                        {
                            RaceDefinition = a.Key.RaceDefinition,
                            RaceType = a.Key.RaceType,
                            Discpline = a.Key.Discpline,
                            FinYear = a.Key.FinYear,
                            EventDate = a.Key.EventDate,
                            Distance = a.Key.Distance,
                            RaceDistanceId = a.Key.RaceDistanceId,

                        })
                        .AsEnumerable();

        }

        public static IEnumerable<RaceResultImportDetailViewModel> ToDetailListViewModel(
        this ICollection<DataAccess.RaceResultImport> entity)
        {
            return entity
                .Where(a => !a.PersonId.HasValue)
                .Select(a =>
                        new RaceResultImportDetailViewModel
                        {
                            Id = a.Id,
                            RaceDefinition = a.RaceDefinition,
                            RaceType = a.RaceType,
                            Discpline = a.Discpline,
                            Province = a.Province,
                            FinYear = a.FinYear,
                            EventDate = a.EventDate,
                            Position = a.Position,
                            TimeTaken = a.TimeTaken,
                            Distance = a.Distance,
                            AgeGroup = a.AgeGroup,
                            Organisation = a.Organisation,
                            FirstName = a.FirstName,
                            Surname = a.Surname,
                            FullName = a.FullName,
                            Discriminator = a.Discriminator,
                            RaceDistanceId = a.RaceDistanceId,
                            IsActive = a.IsActive
                        });

        }

        /// <summary>
        /// Convert RaceResultImport Object into RaceResultImport Entity
        /// </summary>
        ///<param name="model">RaceResultImport</param>
        ///<param name="RaceResultImportEntity">DataAccess.RaceResultImport</param>
        ///<returns>DataAccess.RaceResultImport</returns>
        public static DataAccess.RaceResultImport ToEntity(this RaceResultImportViewModel model,
             DataAccess.RaceResultImport entity)
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
            entity.RaceDefinition = model.RaceDefinition;
            entity.RaceType = model.RaceType;
            entity.Discpline = model.Discpline;
            entity.Province = model.Province;
            entity.FinYear = model.FinYear;
            entity.EventDate = model.EventDate;
            entity.Distance = model.Distance;
            entity.AgeGroup = model.AgeGroup;
            entity.Organisation = model.Organisation;
            entity.FirstName = model.FirstName;
            entity.Surname = model.Surname;
            entity.Discriminator = model.Discriminator;
            entity.Position = model.Position;
            return entity;
        }

        /// <summary>
        /// Convert RaceResultImport Entity  into RaceResultImport Object
        /// </summary>
        ///<param name="model">RaceResultImportViewModel</param>
        ///<param name="RaceResultImportEntity">DataAccess.RaceResultImport</param>
        ///<returns>RaceResultImportViewModel</returns>
        public static RaceResultImportViewModel ToViewModel(
         this DataAccess.RaceResultImport entity,
         RaceResultImportViewModel model)
        {
            model.Id = entity.Id;
            model.RaceDefinition = entity.RaceDefinition;
            model.RaceType  = entity.RaceType;
            model.Discpline  = entity.Discpline;
            model.Province  = entity.Province;
            model.FinYear  = entity.FinYear;
            model.EventDate  = entity.EventDate;
            model.Distance  = entity.Distance;
            model.AgeGroup  = entity.AgeGroup;
            model.Organisation  = entity.Organisation;
            model.FirstName  = entity.FirstName;
            model.Surname  = entity.Surname;
            model.FullName = entity.FullName;
            model.Discriminator  = entity.Discriminator;
            model.Position  = entity.Position;
            model.IsActive = entity.IsActive;
            model.RaceDistanceId = entity.RaceDistanceId;
            return model;
        }

        public static IEnumerable<RaceResultImportViewModel> ToQueryListViewModel(
   this IQueryable<DataAccess.RaceResultImport> entity)
        {
            return entity
                .Include(c => c.Person)
                .AsNoTracking()
                .Select(a =>
                        new RaceResultImportViewModel
                        {
                            Id = a.Id,
                            RaceDefinition = a.RaceDefinition,
                            RaceType = a.RaceType,
                            Discpline = a.Discpline,
                            Province = a.Province,
                            FinYear = a.FinYear,
                            EventDate = a.EventDate,
                            Distance = a.Distance,
                            AgeGroup = a.AgeGroup,
                            Organisation = a.Organisation,
                            FirstName = a.FirstName,
                            Surname = a.Surname,
                            FullName = a.FullName,
                            Discriminator = a.Discriminator,
                            IsActive = a.IsActive

                        });
        }

    }
}
