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
    public static class TrainingPlanTransform
    {

        /// <summary>
        /// Convert TrainingPlan Object into TrainingPlan Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.TrainingPlan</param>
        ///<returns>IEnumerable TrainingPlan</returns>
        ///
        public static IEnumerable<TrainingPlanViewModel> ToListViewModel(
            this IQueryable<DataAccess.TrainingPlan> entity)
        {
            return entity
                .Include(c => c.FinYear)
                .Include(c => c.Event)
                .Include(c => c.TrainingPlanDistances).ThenInclude(c => c.Distance)
                .Include(c => c.TrainingPlanMembers).ThenInclude(c => c.Member)
                .Include(c => c.TrainingPlanRaceDefinitions).ThenInclude(a => a.RaceDefinition)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new TrainingPlanViewModel
                        {
                            Id = a.Id,
                            FinYear = a.FinYear.Name,
                            Event = a.Event.Name,
                            Name = a.Name,
                            Objective = a.Objective,
                            DistanceCount = a.TrainingPlanDistances.Count(),
                            MemberCount = a.TrainingPlanMembers.Count(),
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert TrainingPlan Object into TrainingPlan Entity
        /// </summary>
        ///<param name="model">TrainingPlan</param>
        ///<param name="TrainingPlanEntity">DataAccess.TrainingPlan</param>
        ///<returns>DataAccess.TrainingPlan</returns>
        public static DataAccess.TrainingPlan ToEntity(this TrainingPlanViewModel model,
             DataAccess.TrainingPlan entity)
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
            entity.Objective = model.Objective;
            entity.FinYearId = model.FinYearId;
            entity.EventId = model.EventId;
            return entity;
        }

        /// <summary>
        /// Convert TrainingPlan Entity  into TrainingPlan Object
        /// </summary>
        ///<param name="model">TrainingPlanViewModel</param>
        ///<param name="TrainingPlanEntity">DataAccess.TrainingPlan</param>
        ///<returns>TrainingPlanViewModel</returns>
        public static TrainingPlanViewModel ToViewModel(
         this DataAccess.TrainingPlan entity,
         TrainingPlanViewModel model)
        {
            model.Id = entity.Id;
            model.EventId = entity.EventId;
            model.FinYearId = entity.FinYearId;
            model.Name = entity.Name;
            model.Objective = entity.Objective;
            model.FinYear = entity.FinYear.Name;
            model.Event = entity.Event.Name;
            model.MemberIds = entity.TrainingPlanMembers.Select(a => a.MemberId).ToArray();
            model.DistanceIds = entity.TrainingPlanDistances.Select(a => a.DistanceId).ToArray();
            model.RaceDefinitionIds = entity.TrainingPlanRaceDefinitions.Select(a => a.RaceDefinitionId).ToArray();
            return model;
        }

        public static TrainingPlanQLViewModel ToQueryViewModel(
            this DataAccess.TrainingPlan entity,
            TrainingPlanQLViewModel model)
        {
            model.Id = entity.Id;
            model.EventId = entity.EventId;
            model.FinYearId = entity.FinYearId;
            model.Name = entity.Name;
            model.EventName = entity.Event.Name;
             model.Objective = entity.Objective;
             model.DistanceCount = entity.TrainingPlanDistances.Count();
            model.MemberCount = entity.TrainingPlanMembers.Count();
            model.FinYear = entity.FinYear.Name;
            model.Event = entity.Event.ToViewModel(new EventViewModel());
            model.MemberIds = entity.TrainingPlanMembers.Select(a => a.MemberId).ToArray();
            model.DistanceIds = entity.TrainingPlanDistances.Select(a => a.DistanceId).ToArray();
            model.RaceDefinitionIds = entity.TrainingPlanRaceDefinitions.Select(a => a.RaceDefinitionId).ToArray();
            model.RaceDefinitions = entity.TrainingPlanRaceDefinitions.Select(a => a.RaceDefinition.Name);
            model.Distances = entity.TrainingPlanDistances.Select(a => a.Distance.Name);

            model.Members = entity.TrainingPlanMembers.Select(a => a.Member).AsQueryable().ToQueryListViewModel();
            return model;
        }

        public static IEnumerable<TrainingPlanQLViewModel> ToQueryListViewModel(
           this IQueryable<DataAccess.TrainingPlan> entity)
        {
            return entity
                 .Include(c => c.FinYear)
                .Include(c => c.Event).ThenInclude(c => c.EventType)
                .Include(c => c.Event).ThenInclude(c => c.Frequency)
                .Include(c => c.TrainingPlanDistances).ThenInclude(c => c.Distance)
                .Include(c => c.TrainingPlanRaceDefinitions).ThenInclude(a => a.RaceDefinition)
                 .Include(c => c.TrainingPlanMembers).ThenInclude(c => c.Member.Person.Title)
                .Include(c => c.TrainingPlanMembers).ThenInclude(c => c.Member.Person.Gender)
                 .Include(c => c.TrainingPlanMembers).ThenInclude(c => c.Member.Person.AgeGroup)
                 .Include(c => c.TrainingPlanMembers).ThenInclude(c => c.Member.Person.Document)
                .AsNoTracking()
                .Select(a =>
                        new TrainingPlanQLViewModel
                        {
                            Id = a.Id,
                            FinYear = a.FinYear.Name,
                           // Event = a.Event.ToViewModel(new EventViewModel()),
                            EventName = a.Event.Name,
                            Name = a.Name,
                            Objective = a.Objective,
                            DistanceCount = a.TrainingPlanDistances.Count(),
                            MemberCount = a.TrainingPlanMembers.Count(),
                            IsActive = a.IsActive,
                        });
        }
    }

}
