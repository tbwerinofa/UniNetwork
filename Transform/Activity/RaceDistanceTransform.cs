using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class RaceDistanceTransform
    {

        public static IEnumerable<RaceDistanceViewModel> ToListViewModel(
         this IQueryable<DataAccess.RaceDistance> entity)
        {
            return entity
                .Include(c => c.Distance)
                 .Include(c => c.Race)
                .AsNoTracking()
                .Select(a =>
                        new RaceDistanceViewModel
                        {
                            Id = a.Id,
                            DistanceId = a.DistanceId,
                            RaceId = a.RaceId,
                            EventDateTimes = a.EventDate,
                            IsActive = a.IsActive,
                            Distance = a.Distance.Name,
                            Race = a.Race.RaceDefinition.Name
                            
                        });
        }
        
        public static RaceDistance ToEntity(this RaceDistance entity, int referralId, int containerId,DateTime eventDateTime, string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.RaceId = containerId;
                entity.DistanceId = referralId;
                entity.CreatedUserId = sessionUserID;
                entity.EventDate = eventDateTime;
            }



            return entity;
        }

        public static IEnumerable<RaceDistanceViewModel> ToQueryListViewModel(
        this IQueryable<DataAccess.RaceDistance> entity)
        {
            return entity
                .Include(c => c.Distance)
                 .Include(c => c.Race.RaceDefinition)
                 .Include(c => c.RaceResults).ThenInclude(c => c.Member.Person.Gender)
                  .Include(c => c.RaceResults).ThenInclude(c => c.Member.Person.AgeGroup)
                 .Include(c => c.RaceResults).ThenInclude(c => c.RaceDistance.Distance)

                .AsNoTracking()
                .Select(a =>
                        new RaceDistanceViewModel
                        {
                            Id = a.Id,
                            DistanceId = a.DistanceId,
                            RaceId = a.RaceId,
                            EventDateTimes = a.EventDate,
                            IsActive = a.IsActive,
                            Distance = a.Distance.Name,
                            Race = a.Race.RaceDefinition.Name,
                            Participant = a.RaceResults.Count(),
                            RaceResults = a.RaceResults.AsQueryable().ToQueryListViewModel()
                        });
        }
    }
}
