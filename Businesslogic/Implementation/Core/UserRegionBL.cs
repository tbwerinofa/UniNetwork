using BusinessLogic.Interface;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    //public class UserRegionBL:IUserRegionBL
    //{

    //    protected readonly SqlServerApplicationDbContext _context;

  


    //    #region Constructors
    //    public UserRegionBL(SqlServerApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    #endregion

    //    public async Task<SaveResult> SaveEntityList(UserViewModel model, ApplicationUser user)
    //    {
    //        SaveResult saveResult = new SaveResult();

    //        var currentEntities = user.UserRegions;

    //        if (model.RegionIds == null)
    //        {

    //            if (currentEntities.Any())
    //            {
    //                var userRegionIds = currentEntities.ToList().ToList().Select(b => b.Id);

    //                var toDeleteList = _context.UserRegion.Where(a => userRegionIds.Contains(a.Id));

    //                _context.UserRegion.RemoveRange(toDeleteList);
    //                await _context.SaveChangesAsync();
    //                saveResult.IsSuccess = true;
    //            }
    //            else
    //            {
    //                saveResult.IsSuccess = true;
    //            }
    //        }
    //        else
    //        {

    //            var regions = _context.Province.Where(a => model.RegionIds.Contains(a.Id));

    //            if (currentEntities.Any())
    //            {

    //                saveResult.IsSuccess = await AddUserRegionWherePreviousExists(model, currentEntities, regions);

    //                if (saveResult.IsSuccess)
    //                {
    //                    saveResult = await ManageExistingUserRegion(model, currentEntities, regions);
    //                }
    //            }
    //            else
    //            {

    //                saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
    //            }
    //        }


    //        return saveResult;
    //    }

    //    private async Task<SaveResult> ManageExistingUserRegion(UserViewModel model,
    //     ICollection<UserRegion> currentEntityList,
    //     IQueryable<Province> roleAppPage)
    //    {
    //        SaveResult saveResult = new SaveResult
    //        {
    //            IsSuccess = true
    //        };

    //        List<UserRegion> assignedUserRegions = new List<UserRegion>();

    //        foreach (var record in currentEntityList)
    //        {
    //            if (saveResult.IsSuccess)
    //            {
    //                if (!model.RegionIds.Any(a => a == record.RegionId))
    //                {
    //                    var deleteUserRegion = await _context.UserRegion.FindAsync(record.Id);

    //                    if (saveResult.IsSuccess)
    //                    {
    //                         _context.Remove(deleteUserRegion);
    //                        await _context.SaveChangesAsync();
    //                        saveResult.IsSuccess = true;
    //                    }
    //                }
    //                else
    //                {
    //                    saveResult = await EditEntityAsync(model, record);
    //                }
    //            }
    //        }
    //        return saveResult;
    //    }

    //    private async Task<bool> AddUserRegionWherePreviousExists(UserViewModel model,
    //ICollection<UserRegion> userRegions,
    //IQueryable<Province> regions)
    //    {
    //        bool isSaveSuccess = true;
    //        List<UserRegion> assignedUserRegions = new List<UserRegion>();
    //        foreach (var regionId in model.RegionIds)
    //        {

    //            var currentRolePermission = regions.Where(a => a.Id == regionId).FirstOrDefault();
    //            if (currentRolePermission != null)
    //            {

    //                if (!userRegions.Any(a => a.RegionId == regionId))
    //                {
    //                    // var currentUserRoleID = currentMenuRoles.First(a => a.Region.RoleID == currentRolePermission.RoleID).RegionID;
    //                    var UserRegionRepo = new UserRegion();
    //                    assignedUserRegions.Add(UserRegionRepo.ToEntity(regionId, model.Id, model.SessionUserId));
    //                }
    //            }


    //        }

    //        if (assignedUserRegions.Count > 0)
    //        {
    //            await _context.UserRegion.AddRangeAsync(assignedUserRegions);
    //            await _context.SaveChangesAsync();
    //            isSaveSuccess = true;
    //        }

    //        return isSaveSuccess;
    //    }


    //    private async Task<SaveResult> EditEntityAsync(UserViewModel model, UserRegion record)
    //    {
    //        var saveResult = new SaveResult();
    //        var editUserRegion = await _context.UserRegion.FindAsync(record.Id);
    //        editUserRegion.ToEntity(record.RegionId, model.Id, model.SessionUserId);

    //        _context.Update(editUserRegion);
    //        await _context.SaveChangesAsync();

    //        saveResult.IsSuccess = true;
    //        return saveResult;

    //    }

    //    private async Task<bool> AddEntity(UserViewModel model,
    //           ICollection<UserRegion> userRegions,
    //           IQueryable<Province> regions)
    //    {

    //        bool isSaveSuccess = true;
    //        List<UserRegion> userRegionList = new List<UserRegion>();
    //        foreach (var record in model.RegionIds)
    //        {
    //            var currentRolePermission = regions.Any(a => a.Id == record);
    //            if (currentRolePermission)
    //            {
    //                UserRegion userRegion = new UserRegion();
    //                userRegionList.Add(userRegion.ToEntity(record, model.Id, model.SessionUserId));
    //            }
    //        }

    //        if (userRegionList.Count > 0)
    //        {
    //            await _context.UserRegion.AddRangeAsync(userRegionList);
    //            await _context.SaveChangesAsync();
    //            isSaveSuccess = true;
    //        }

    //        return isSaveSuccess;
    //    }
    //}
}
