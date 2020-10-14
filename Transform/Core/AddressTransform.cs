using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class AdressTransform
    {

        /// <summary>
        /// Convert Address Object into Address Entity
        /// </summary>
        ///<param name="model">Address</param>
        ///<param name="AddressEntity">DataAccess.Address</param>
        ///<returns>DataAccess.Address</returns>
        public static Address ToEntity(this AddressViewModel model,Address entity
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

            entity.Line1 = model.Line1;
            entity.Line2 = model.Line2;
            entity.SuburbId = model.SuburbId;


            return entity;
        }

        public static AddressViewModel ToViewModel(this DataAccess.Address entity,
           AddressViewModel model)
        {
            model.Id = entity.Id;
            model.Line1 = entity.Line1;
            model.Line2 = entity.Line2;
            model.Code = entity.Code;
            model.Id = entity.Id;
            model.CountryId = entity.Suburb.Town.City.Province.CountryId;
            model.ProvinceId = entity.Suburb.Town.City.ProvinceId;
            model.CityId = entity.Suburb.Town.CityId;
            model.TownId = entity.Suburb.TownId;
            model.SuburbId = entity.SuburbId;
            model.IsActive = entity.IsActive;
            model.Country = entity.Suburb.Town.City.Province.Country.Name;
            model.Province = entity.Suburb.Town.City.Province.Name;
            model.City = entity.Suburb.Town.City.Name;
            model.Town = entity.Suburb.Town.Name;
            model.Suburb = entity.Suburb.Name;

            return model;
        }

    }
}
