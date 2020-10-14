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
    public class MemberStagingBL : IEntityViewLogic<MemberStagingViewModel>,IMemberStagingBL
    {

        #region global fields
        private readonly SqlServerApplicationDbContext _context;
       // protected readonly IPersonBL _personBL;
       // protected readonly IQueuedEmailBL _queuedEmailBL;
        #endregion 

        #region Constructors
        public MemberStagingBL(SqlServerApplicationDbContext context
         //   IPersonBL personBL,
         //   IQueuedEmailBL queuedEmailBL
         )
        {
            _context = context;
            //_personBL = personBL;
            //_queuedEmailBL = queuedEmailBL;
        }

        #endregion

        #region Methods

        #region Read

        public ResultSetPage<MemberStagingViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(MemberStagingViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.MemberStaging
                 .IgnoreQueryFilters()
                 .Where(a=> !a.IsFinalised)
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.FirstName.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<MemberStagingViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new MemberStagingViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {

                var test = _context.MemberStaging.FirstOrDefault();
                var entity = await _context.MemberStaging
                    .Include(c => c.Address)
                    .Include(c => c.Address.Suburb.Town.City.Province.Country)
                    .Include(c => c.CreatedUser)
                    .Include(c => c.Country)
                    .Include(c => c.UpdatedUser)
                    .Include(c => c.CreatedUser)
                    .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);

                    viewModel.Address = entity.Address.ToViewModel(new AddressViewModel());

                    viewModel.Address.Cities = this._context.City.Where(a => a.ProvinceId == viewModel.Address.CountryId).ToSelectListItem(a => a.Name, x => x.Id.ToString());


                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(MemberStagingViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            var countries = _context.Country.Include(a => a.Provinces).ThenInclude(a => a.Cities);
            model.Genders = _context.Gender.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Titles = _context.Title.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Nationalities = countries.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.IDTypes = _context.IDType.ToSelectListItem(a => a.Name, x => x.Id.ToString(), true, excludeSort: true).OrderBy(a => a.Value);
            model.Address = new AddressViewModel
            {
                Countries = countries.Where(a=> a.Provinces.Any()).ToSelectListItem(a => a.Name, x => x.Id.ToString()),
                Provinces = selectListItem,
                Cities = selectListItem,
                Towns = selectListItem,
                Suburbs = selectListItem
            };

        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(MemberStagingViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            bool isNew = false;
                var entity = new MemberStaging();
            viewModel.Address.SessionUserId = viewModel.SessionUserId;
         
            try
                {
                    saveResult = await SaveAddress(viewModel);

                if (saveResult.IsSuccess)
                {

                    if (_context.MemberStaging.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.MemberStaging.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                        entity = viewModel.ToEntity(entity);
                        _context.MemberStaging.Update(entity);
                    }
                    else
                    {
                        isNew = true;
                        entity = viewModel.ToEntity(entity);
                        _context.MemberStaging.Add(entity);
                    }
                    await _context.SaveChangesAsync();

                    //if(isNew)
                    //{
                    //    await _queuedEmailBL.GenerateRegistrationNotificationEmail(viewModel, MessageTemplateConst.RegistrationNotification);
                    //}
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

                    saveResult.Message = CrudError.DeleteErrorMsg;
                }


                return saveResult;
            }

        private async Task<SaveResult> SaveAddress(MemberStagingViewModel viewModel)
        {
            try
            {


                SaveResult saveResult = new SaveResult();

                var entity = new Address();

                if (viewModel.Address.Id != 0)
                {
                    if (_context.Address.Any(a => a.Id == viewModel.Address.Id))
                    {
                        entity = await _context.Address.FirstOrDefaultAsync(a => a.Id == viewModel.Address.Id);
                    }
                    entity = viewModel.Address.ToEntity(entity);
                    _context.Address.Update(entity);
                }
                else
                {
                    entity = viewModel.Address.ToEntity(entity);
                    _context.Address.Add(entity);
                }

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                }
                saveResult.Id = entity.Id;
                viewModel.AddressId = entity.Id;
                viewModel.Address.Id = viewModel.AddressId;

                return saveResult;
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SaveResult> VerifyModel(MemberStagingViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var entity = new MemberStaging();
  

            try
            {

                if (_context.MemberStaging.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                {
                    entity = await _context.MemberStaging.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    entity.IsFinalised = viewModel.IsFinalised;

                    if (!entity.IsFinalised)
                    {
                        entity.Comment = viewModel.Comments;
                        entity.IsRejected = true;
                    }
                    _context.MemberStaging.Update(entity);

                    await _context.SaveChangesAsync();
                    saveResult.IsSuccess = true;

                    //send approval email
                    entity.ToViewModel(viewModel);
                    //allow user to create an account
                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;

                        if (viewModel.IsFinalised)
                        {
                            //saveResult = await _personBL.GenerateEntity(viewModel);

                            //if (saveResult.IsSuccess)
                            //{
                            //    saveResult = await this.DeleteEntity(entity.Id);
                            //}
                        }
                        else
                        {
                            saveResult = await this.DeleteEntity(entity.Id);
                        }

                    }
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

                saveResult.Message = CrudError.DeleteErrorMsg;
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
                var entity = await _context.MemberStaging.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.MemberStaging.Remove(entity);
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
