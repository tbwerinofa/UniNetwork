using BusinessObject;
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
    public static class MemberLicenseTransform
    {
        /// <summary>
        /// Convert Corporate Object into Corporate Entity
        /// </summary>
        ///<param name="model">Corporate</param>
        ///<param name="CorporateEntity">DataAccess.Corporate</param>
        ///<returns>DataAccess.Corporate</returns>
        public static MemberLicense ToEntity(this MemberLicenseViewModel model,MemberLicense entity
           )
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
            entity.FinYearId = model.FinYearId;
            entity.MemberId = model.MemberId;
            entity.LicenseNo = model.LicenseNo;

            return entity;
        }

        public static IEnumerable<MemberLicenseViewModel> ToListViewModel(
           this IQueryable<MemberLicense> entity)
        {
            return entity
                .Include(c => c.Member.Person)
                 .Include(c => c.FinYear)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .AsNoTracking()
                .ToList().Select(a =>
                        new MemberLicenseViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            FinYearId = a.FinYearId,
                            MemberId = a.MemberId,
                            FullName = a.Member.Person.FullName,
                            MemberNo = a.Member.MemberNo,
                            LicenseNo = a.LicenseNo,
                            FinYear = a.FinYear.Name,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                         
                        });
        }

        /// <summary>
        /// Convert Corporate Entity  into Corporate Object
        /// </summary>
        ///<param name="model">MemberLicenseViewModel</param>
        ///<param name="CorporateEntity">DataAccess.Corporate</param>
        ///<returns>MemberLicenseViewModel</returns>
        public static MemberLicenseViewModel ToViewModel(this MemberLicense entity,
            MemberLicenseViewModel model)
        {
            model.Id = entity.Id;
           model.FinYearId = entity.FinYearId;
           model.MemberId = entity.MemberId;
           model.MemberNo = entity.Member.MemberNo;
           model.FullName = entity.Member.Person.FullName;
           model.FinYear = entity.FinYear.Name;
            model.LicenseNo = entity.LicenseNo;
            return model;
        }


    }
}
