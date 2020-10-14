using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class MemberLicenseBL :IEntityViewLogic<MemberLicenseViewModel>
    {
        #region global fields

        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        #endregion

        #region Constructors
        public MemberLicenseBL(SqlServerApplicationDbContext context,
            IFinYearBL finYearBL)
        {
            _context = context;
            _finYearBL = finYearBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<MemberLicenseViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(MemberLicenseViewModel).GetProperty(paramList.SortField);

            var data = _context.MemberLicense;

            var resultSet = _context.MemberLicense
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.LicenseNo.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<MemberLicenseViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new MemberLicenseViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.MemberLicense
                    .IgnoreQueryFilters()
                      .Include(c => c.FinYear)
                      .Include(c => c.Member.Person)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(MemberLicenseViewModel model)
        {
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();
            model.Members = _context.Member
                                .Include(a => a.Person)
                                .ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString());
        }




        #endregion

        #region Create/Update

        public async Task<SaveResult> SaveEntity(MemberLicenseViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var entity = new MemberLicense();

            try
            {

                if (viewModel.Id != 0)
                {
                    if (_context.MemberLicense.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.MemberLicense.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.MemberLicense.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.MemberLicense.Add(entity);
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

        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.MemberLicense.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.MemberLicense.Remove(entity);
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