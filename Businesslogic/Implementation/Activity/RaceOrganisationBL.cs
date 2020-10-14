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
    public class RaceOrganisationBL:IRaceOrganisationBL
    {

        protected readonly SqlServerApplicationDbContext _context;

  


        #region Constructors
        public RaceOrganisationBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<SaveResult> SaveEntityList(RaceViewModel model, Race parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            List<int> organisationIds = new List<int>
            {
                model.OrganisationId
            };
            model.OrganisationIds = organisationIds;

            var currentEntities = parentEntity.RaceOrganisations;

            if (model.OrganisationIds == null)
            {

                if (currentEntities.Any())
                {
                    var RaceOrganisationIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.RaceOrganisation.Where(a => RaceOrganisationIds.Contains(a.Id));

                    _context.RaceOrganisation.RemoveRange(toDeleteList);
                    await _context.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
                else
                {
                    saveResult.IsSuccess = true;
                }
            }
            else
            {

                var regions = _context.Organisation.Where(a => model.OrganisationIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddRaceOrganisationWherePreviousExists(model, currentEntities, regions);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingRaceOrganisation(model, currentEntities, regions);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, regions);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingRaceOrganisation(RaceViewModel model,
         ICollection<RaceOrganisation> currentEntityList,
         IQueryable<Organisation> roleAppPage)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<RaceOrganisation> assignedRaceOrganisations = new List<RaceOrganisation>();

            foreach (var record in currentEntityList)
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.OrganisationIds.Any(a => a == record.OrganisationId))
                    {
                        var deleteRaceOrganisation = await _context.RaceOrganisation.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                             _context.Remove(deleteRaceOrganisation);
                            await _context.SaveChangesAsync();
                            saveResult.IsSuccess = true;
                        }
                    }
                    else
                    {
                        saveResult = await EditEntityAsync(model, record);
                    }
                }
            }
            return saveResult;
        }

        private async Task<bool> AddRaceOrganisationWherePreviousExists(RaceViewModel model,
    ICollection<RaceOrganisation> RaceOrganisations,
    IQueryable<Organisation> regions)
        {
            bool isSaveSuccess = true;
            List<RaceOrganisation> assignedRaceOrganisations = new List<RaceOrganisation>();
            foreach (var recordId in model.OrganisationIds)
            {

                var currentRolePermission = regions.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!RaceOrganisations.Any(a => a.OrganisationId == recordId))
                    {
                        // var currentUserRoleID = currentMenuRoles.First(a => a.Organisation.RoleID == currentRolePermission.RoleID).RegionID;
                        var RaceOrganisationRepo = new RaceOrganisation();
                        assignedRaceOrganisations.Add(RaceOrganisationRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }

            if (assignedRaceOrganisations.Count > 0)
            {
                await _context.RaceOrganisation.AddRangeAsync(assignedRaceOrganisations);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(RaceViewModel model, RaceOrganisation record)
        {
            var saveResult = new SaveResult();
            var editRaceOrganisation = await _context.RaceOrganisation.FindAsync(record.Id);
            editRaceOrganisation.ToEntity(record.OrganisationId, model.Id, model.SessionUserId);

            _context.Update(editRaceOrganisation);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(RaceViewModel model,
               ICollection<RaceOrganisation> raceOrganisations,
               IQueryable<Organisation> regions)
        {

            bool isSaveSuccess = true;
            List<RaceOrganisation> RaceOrganisationList = new List<RaceOrganisation>();
            foreach (var record in model.OrganisationIds)
            {
                var currentRolePermission = regions.Any(a => a.Id == record);
                if (currentRolePermission)
                {
                    RaceOrganisation raceOrganisation = new RaceOrganisation();
                    RaceOrganisationList.Add(raceOrganisation.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (RaceOrganisationList.Count > 0)
            {
                await _context.RaceOrganisation.AddRangeAsync(RaceOrganisationList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
    }
}
