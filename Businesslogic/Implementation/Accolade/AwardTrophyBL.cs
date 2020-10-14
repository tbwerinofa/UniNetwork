using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class AwardTrophyBL : IEntityViewLogic<AwardTrophyViewModel>
    {
        #region Global fields
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        #endregion

        #region Constructors
        public AwardTrophyBL(SqlServerApplicationDbContext context,
            IFinYearBL finYearBL)
        {
            _context = context;
            _finYearBL = finYearBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<AwardTrophyViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(AwardTrophyViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.AwardTrophy
                                .Include(a=> a.FinYear)
                                .Include(a => a.Award)
                                .Include(a => a.Trophy)
                                .Include(c => c.Award.Gender)
                                .Include(a => a.CreatedUser)
                                .Include(a => a.UpdatedUser)
                                .IgnoreQueryFilters()
                                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.Award.Name.Contains(paramList.SearchTerm))
                                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<AwardTrophyViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new AwardTrophyViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.AwardTrophy
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }

        private void PopulateDropDowns(AwardTrophyViewModel model)
        {
            model.Trophies = _context.Trophy.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Awards = _context.Award.Include(a=>a.Gender)
                .ToSelectListItem(a => a.Name + ( a.Gender!=null? " Gender: " + a.Gender.Name + ",": string.Empty) + " Position:" + a.Ordinal + "", x => x.Id.ToString());
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();
            model.StartDate = DateTime.Now;
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.AwardTrophy
                .Include(a=>a.Award)
                .ToSelectListItem(x => x.Award.Name.ToString(),
                                                     x => x.Id.ToString());
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(AwardTrophyViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new AwardTrophy();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.AwardTrophy.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.AwardTrophy.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }

                         MapToAudit(entity,viewModel);
                        _context.AwardTrophy.Update(entity);
                    }
                    else
                    {
                    MapToAudit(entity, viewModel);
                    _context.AwardTrophy.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                    }

                }
                catch (DbUpdateException upDateEx)
                {
                    var results = upDateEx.GetSqlerrorNo();
                    string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                    saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();

                }
                catch (Exception ex)
                {

                    saveResult.Message = CrudError.SaveErrorMsg;
                }


                return saveResult;
            }


        private void MapToAudit(AwardTrophy dbentity,AwardTrophyViewModel model)
        {

            bool hasChanged = false;

            if (
            (dbentity.FinYearId != model.FinYearId) ||
            (dbentity.TrophyId != model.TrophyId)||
             (dbentity.AwardId != model.AwardId)
            )
            {
                hasChanged = true;
            }


            var entity = model.ToEntity(dbentity);


            entity.AwardTrophyAudits
                .ToList()
                .ForEach(a =>
                a.IsActive = false);

            if (hasChanged)
            {
                var audit = entity.ToAuditEntity();
                entity.AwardTrophyAudits.Add(audit);
            }

        }

        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.AwardTrophy.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.AwardTrophy.Remove(entity);
                await _context.SaveChangesAsync();

                resultSet.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();

                string msg = results == (int)SqlErrNo.FK ? ConstEntity.ForeignKeyDelMsg : CrudError.DeleteErrorMsg;
                resultSet = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                resultSet.Message = CrudError.DeleteErrorMsg;
            }
            return resultSet;

        }
        #endregion

        #endregion

    }
}
