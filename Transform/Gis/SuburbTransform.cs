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
    public static class SuburbTransform
    {


        /// <summary>
        /// Convert Suburb Object into Suburb Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Suburb</param>
        ///<returns>IEnumerable Suburb</returns>
        ///
        public static IEnumerable<SuburbViewModel> ToListViewModel(
            this IQueryable<DataAccess.Suburb> entity)
        {
            return entity
                .Include(c => c.Town)
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new SuburbViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            TownId = a.TownId,
                            Town = a.Town.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Suburb Object into Suburb Entity
        /// </summary>
        ///<param name="model">Suburb</param>
        ///<param name="SuburbEntity">DataAccess.Suburb</param>
        ///<returns>DataAccess.Suburb</returns>
        public static Suburb ToEntity(this SuburbViewModel model,
            Suburb entity)
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
            entity.TownId = model.TownId;
            entity.Name = model.Name;
            return entity;
        }

        /// <summary>
        /// Convert Suburb Entity  into Suburb Object
        /// </summary>
        ///<param name="model">SuburbViewModel</param>
        ///<param name="SuburbEntity">DataAccess.Suburb</param>
        ///<returns>SuburbViewModel</returns>
        public static SuburbViewModel ToViewModel(
         this DataAccess.Suburb entity,
         SuburbViewModel model)
        {
            model.Id = entity.Id;
            model.TownId = entity.TownId;
            model.Name = entity.Name;
            model.Town = entity.Town.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

        /// <summary>
        /// Transform IQueryable DataAccess.SuburbSuburb into IEnumerable DropDownListItems
        /// </summary>
        ///<param name="gisSuburbSuburb">IQueryable DataAccess.SuburbSuburb</param>
        ///<returns>IEnumerable DropDownListItems</returns>
        public static IEnumerable<DropDownListItems> ToSuburbDropDownListItems(
            this IQueryable<DataAccess.Suburb> gisSuburbSuburb)
        {

            return gisSuburbSuburb
                    .Select(a => new DropDownListItems
                    {
                        Value = a.Id,
                        Text = a.Name,
                    })
                    .Distinct()
                    .OrderBy(a => a.Text)
                    .ToList();

        }

        /// <summary>
        /// Transform IQueryable DataAccess.SuburbSuburb into IEnumerable Suburb DropDownListItems
        /// </summary>
        ///<param name="gisSuburbSuburb">IQueryable DataAccess.SuburbSuburb</param>
        ///<returns>IEnumerable DropDownListItems</returns>
        public static IEnumerable<DropDownListItems> ToDropDownListItems(
            this IQueryable<DataAccess.Suburb> gisSuburbSuburb)
        {

            return gisSuburbSuburb
                    .Select(a => new DropDownListItems
                    {
                        Value = (int)a.Id,
                        Text = a.PostCode,
                        DataLatitude = a.Latitude.ToString(),
                        DataLongitude = a.Longitude.ToString(),
                    })
                    .Distinct()
                    .OrderBy(a => a.Text)
                    .ToList();

        }


    }
}
