using BusinessObject;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class MenuTransform
    {
        /// <summary>
        /// Convert Menu Object into Menu Entity
        /// </summary>
        ///<param name="model">Menu</param>
        ///<param name="MenuEntity">DataAccess.Menu</param>
        ///<returns>DataAccess.Menu</returns>
        public static Menu ToEntity(this MenuViewModel model,Menu entity
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
            entity.Name = model.Menu;
            entity.Controller = model.Controller;
            entity.ActionResult = model.ActionResult;
            entity.Icon = model.Icon;
            entity.Parameter = model.Parameter;
            entity.MenuAreaId = model.MenuAreaId;

            return entity;
        }


        public static IEnumerable<MenuViewModel> ToListViewModel(
       this IQueryable<Menu> entity)
        {
            return entity

                .Include(c => c.MenuArea)
                 .Include(c => c.MenuArea.DefaultMenu)
                .Include(c => c.MenuSection)
                .Include(c => c.MenuSection.MenuGroup)
                   .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .ToList().Select(a =>
                        new MenuViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            Menu = a.Name,
                            Controller = a.Controller,
                            MenuGroupId = a.MenuSection.MenuGroupId,
                            MenuGroup = a.MenuSection.MenuGroup.Name,
                            MenuGroupIcon = a.MenuSection.MenuGroup.Icon,
                            Icon = a.Icon,
                            ActionResult = a.ActionResult,
                            MenuSection = a.MenuSection.Name,
                            MenuArea = a.MenuArea.Name,
                            MenuAreaReference = a.MenuArea.Discriminator,
                            MenuAreaOrdinal = a.MenuArea.Ordinal,
                            MenuGroupOrdinal = a.MenuSection.MenuGroup.Ordinal,
                            MenuSectionOrdinal = a.MenuSection.Ordinal,
                            MenuOrdinal = a.Ordinal,
                            Navigable = a.MenuArea.Navigable,
                            MenuSectionId = a.MenuSectionId,
                            MenuAreaId = a.MenuAreaId,
                            Parameter = a.Parameter,
                            HasArea = a.MenuAreaId.HasValue
                           
                            //LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        public static IEnumerable<MenuViewModel> ToListViewModel(
           this IQueryable<ApplicationRoleMenu> entity,
           string menuAreaDiscriminator,
           string sessionUser,
           string sessionUserId)
        {
            return entity

                .Include(c => c.Menu.MenuArea)
                 .Include(c => c.Menu.MenuArea.DefaultMenu)
                .Include(c => c.Menu.MenuSection)
                .Include(c => c.Menu.MenuSection.MenuGroup)
                .Include(c => c.Menu.ApplicationRoleMenus)
                   .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .ToList()
                .Select(a =>
                        new MenuViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Menu.Id,
                            Menu = a.Menu.Name,
                            Controller = a.Menu.Controller,
                            MenuGroupId = a.Menu.MenuSection.MenuGroupId,
                            MenuGroup = a.Menu.MenuSection.MenuGroup.Name,
                            MenuGroupIcon = a.Menu.MenuSection.MenuGroup.Icon,
                            Icon = a.Menu.Icon,
                            ActionResult = a.Menu.ActionResult,
                            MenuSection = a.Menu.MenuSection.Name,
                            MenuArea = a.Menu.MenuArea.Name,
                            MenuAreaReference = a.Menu.MenuArea.Discriminator,
                            ActiveMenuArea = menuAreaDiscriminator == a.Menu.MenuArea.Discriminator ? true : false,
                            MenuAreaOrdinal = a.Menu.MenuArea.Ordinal,
                            MenuGroupOrdinal = a.Menu.MenuSection.MenuGroup.Ordinal,
                            MenuSectionOrdinal = a.Menu.MenuSection.Ordinal,
                            MenuOrdinal = a.Menu.Ordinal,
                            Navigable = a.Menu.MenuArea.Navigable,
                            FullName = sessionUser,
                            MenuSectionId = a.Menu.MenuSectionId,
                            DefaultController = a.Menu.MenuArea.DefaultMenu?.Controller,
                            DefaultActionResult = a.Menu.MenuArea.DefaultMenu?.ActionResult,
                            DefaultMenu = a.Menu.MenuArea.DefaultMenu?.Name,
                            DefaultMenuId = a.Menu.MenuArea.DefaultMenu?.Id,
                            MenuAreaId = a.Menu.MenuAreaId,
                            Parameter = a.Menu.Parameter,
                            HasArea = a.Menu.MenuAreaId.HasValue,
                            //RoleId = a.ApplicationRoleId,
                            UserId = sessionUserId,
                            //LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        })
                          .GroupBy( a => new 
                {
                    a.Id,
                    a.Menu,
                    a.Controller,
                    a.MenuGroupId,
                    a.MenuGroup,
                    a.MenuGroupIcon,
                    a.Icon,
                    a.ActionResult,
                    a.MenuSection,
                    a.MenuArea,
                    a.MenuAreaReference,
                    a.ActiveMenuArea,
                    a.MenuAreaOrdinal,
                    a.MenuGroupOrdinal,
                    a.MenuSectionOrdinal,
                    a.MenuOrdinal,
                    a.Navigable,
                    a.FullName,
                    a.MenuSectionId,
                    a.DefaultController,
                    a.DefaultActionResult,
                    a.DefaultMenu,
                    a.DefaultMenuId,
                    a.MenuAreaId,
                    a.Parameter,
                    a.HasArea,
                    a.UserId
                })
                .Select(
                a => new MenuViewModel
                {
               Id=     a.Key.Id,
               Menu=     a.Key.Menu,
               Controller=     a.Key.Controller,
               MenuGroupId=     a.Key.MenuGroupId,
               MenuGroup=     a.Key.MenuGroup,
               MenuGroupIcon=     a.Key.MenuGroupIcon,
               Icon=     a.Key.Icon,
               ActionResult=     a.Key.ActionResult,
               MenuSection=     a.Key.MenuSection,
               MenuArea=     a.Key.MenuArea,
               MenuAreaReference=     a.Key.MenuAreaReference,
               ActiveMenuArea=     a.Key.ActiveMenuArea,
               MenuAreaOrdinal=     a.Key.MenuAreaOrdinal,
               MenuGroupOrdinal=     a.Key.MenuGroupOrdinal,
               MenuSectionOrdinal=     a.Key.MenuSectionOrdinal,
               MenuOrdinal=     a.Key.MenuOrdinal,
               Navigable=     a.Key.Navigable,
               FullName=     a.Key.FullName,
               MenuSectionId=     a.Key.MenuSectionId,
               DefaultController=     a.Key.DefaultController,
               DefaultActionResult=     a.Key.DefaultActionResult,
               DefaultMenu=     a.Key.DefaultMenu,
               DefaultMenuId=     a.Key.DefaultMenuId,
               MenuAreaId=     a.Key.MenuAreaId,
               Parameter=     a.Key.Parameter,
               HasArea=     a.Key.HasArea,
               UserId  =   a.Key.UserId
                });
        }

        /// <summary>
        /// Convert Menu Entity  into Menu Object
        /// </summary>
        ///<param name="model">MenuViewModel</param>
        ///<param name="MenuEntity">DataAccess.Menu</param>
        ///<returns>MenuViewModel</returns>
        public static MenuViewModel ToViewModel(this DataAccess.Menu entity,
            MenuViewModel model)
        {
            model.Menu = entity.Name;
            model.Controller = entity.Controller;
            model.ActionResult = entity.ActionResult;
            model.Icon = entity.Icon;
            model.Parameter = entity.Parameter;
            model.MenuAreaId = entity.MenuAreaId;

            return model;
        }
    }
}
