using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class ModeratorTransform
    {

        public static IEnumerable<ModeratorViewModel> ToListViewModel(
         this IQueryable<DataAccess.Moderator> entity)
        {
            return entity
                .Include(c => c.Calendar)
                 .Include(c => c.Member)
                .AsNoTracking()
                .Select(a =>
                        new ModeratorViewModel
                        {
                            Id = a.Id,
                            CalendarId = a.CalendarId,
                            MemberId = a.MemberId,
                            IsActive = a.IsActive,
                            
                        });
        }
        
        public static Moderator ToEntity(this Moderator entity, int referralId, int containerId,string sessionUserId)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.CalendarId = containerId;
                entity.MemberId = referralId;
                entity.CreatedUserId = sessionUserId;

            }



            return entity;
        }

        public static IEnumerable<ModeratorViewModel> ToQueryListViewModel(
        this IQueryable<DataAccess.Moderator> entity)
        {
            return entity
                .Include(c => c.Member.Person.Gender)
                 .Include(c => c.Calendar.Event.EventType)

                .AsNoTracking()
                .Select(a =>
                        new ModeratorViewModel
                        {
                            Id = a.Id,
                            CalendarId = a.MemberId,
                            MemberId = a.MemberId,
                            FullName = a.Member.Person.FullName,
                            IsActive = a.IsActive,
                        });
        }
    }
}
