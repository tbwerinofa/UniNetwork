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
    public class MemberBL : IEntityViewLogic<MemberViewModel>,IMemberBL
    {
        #region global fields

        protected readonly SqlServerApplicationDbContext _context;
        private readonly IPersonBL _personBL;
        private readonly IMemberMappingBL _memberMappingBL;
        #endregion

        #region Constructors
        public MemberBL(SqlServerApplicationDbContext context,
            IPersonBL personBL,
            IMemberMappingBL memberMappingBL)
        {
            _context = context;
            _personBL = personBL;
            _memberMappingBL = memberMappingBL;

        }

        #endregion

        #region Methods

        #region Read

        public IEnumerable<MemberViewModel> GetEntityList()
        {

            var resultSet = _context.Member
                 .IgnoreQueryFilters()
                .ToListViewModel();

            return  resultSet;

        }

        public ResultSetPage<MemberViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(MemberViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Member
                 .IgnoreQueryFilters()
                   .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.MemberNo.Contains(paramList.SearchTerm.ToLower())
                       || a.Person.FullName.ToLower().Contains(paramList.SearchTerm.ToLower())
                       || a.Person.AgeGroup.Name.ToLower().Contains(paramList.SearchTerm.ToLower())
                       || a.Person.Gender.Name.ToLower().Contains(paramList.SearchTerm.ToLower())
                       )
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<MemberViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new MemberViewModel
            {
                Id =Id??0
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {

                var test = _context.Member.FirstOrDefault();
                var entity = await _context.Member
                    .Include(c => c.Person.Address)
                    .Include(c => c.Person.Address.Suburb.Town.City.Province.Country)
                       .Include(c => c.Person.Country)
                    .Include(c => c.Person.Title)
                    .Include(c => c.Person.Gender)
                     .Include(c => c.Person.Document)
                    .Include(c => c.Person.AgeGroup)
                    .Include(c => c.MemberMappings)
                    .Include(c => c.UpdatedUser)
                    .Include(c => c.CreatedUser)
                    .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);

                    viewModel.Address = entity.Person.Address.ToViewModel(viewModel.Address);
                    viewModel.Address.Provinces = this._context.Province.Where(a => a.CountryId == viewModel.Address.CountryId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Cities = this._context.City.Where(a => a.ProvinceId == viewModel.Address.ProvinceId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Towns = this._context.Town.Where(a => a.CityId == viewModel.Address.CityId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Suburbs = this._context.Suburb.Where(a => a.TownId == viewModel.Address.TownId).ToSelectListItem(a => a.Name + "(" + a.StreetCode + ")", x => x.Id.ToString());

                    if (entity.MemberMappings.Any())
                    {
                        viewModel.MemberIds = entity.MemberMappings.Select(a => a.RelationMemberId);
                    }
                }
            }
            return viewModel;
        }

        public async Task<MemberViewModel> GetEntityByUserId(
           string userId)
        {

            var viewModel = new MemberViewModel
            {
                Address = new AddressViewModel()
            };

            PopulateDropDowns(viewModel);

                var entity = await _context.Member
                .Include(c => c.Person.Address)
                .Include(c => c.Person.Users)
                .Include(c => c.Person.Address.Suburb.Town.City.Province.Country)
                .Include(c => c.Person.Country)
                .Include(c => c.Person.Title)
                .Include(c => c.Person.Gender)
                .Include(c => c.Person.AgeGroup)
                .Include(c => c.Person.Document)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                    .FirstOrDefaultAsync(a => a.Person.Users.Any(b=>b.Id == userId));
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);

                    viewModel.Address = entity.Person.Address.ToViewModel(viewModel.Address);
                    viewModel.Address.Provinces = this._context.Province.Where(a => a.CountryId == viewModel.Address.CountryId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Cities = this._context.City.Where(a => a.ProvinceId == viewModel.Address.ProvinceId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Towns = this._context.Town.Where(a => a.CityId == viewModel.Address.CityId).ToSelectListItem(a => a.Name, x => x.Id.ToString());
                    viewModel.Address.Suburbs = this._context.Suburb.Where(a => a.TownId == viewModel.Address.TownId).ToSelectListItem(a => a.Name + "(" + a.StreetCode + ")", x => x.Id.ToString());

                }

            return viewModel;
        }

        private void PopulateDropDowns(MemberViewModel model)
        {
            var selectListItem = IQueryableExtensions.Default_SelectListItem();
            var countries = _context.Country.Include(a => a.Provinces).ThenInclude(a => a.Cities);
            model.Genders = _context.Gender.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Titles = _context.Title.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Nationalities = _context.Country.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.IDTypes = _context.IDType.ToSelectListItem(a => a.Name, x => x.Id.ToString()); ;
            model.Address = new AddressViewModel
            {
                Countries = countries.Where(a => a.Provinces.Any()).ToSelectListItem(a => a.Name, x => x.Id.ToString()),
                Provinces = selectListItem,
                Cities = selectListItem,
                Towns = selectListItem,
                Suburbs = selectListItem
            };

            if(model.Id !=0)
            {
                model.Members =_context.Member.Where(a => a.Id != model.Id)
                .Include(a => a.Person.Gender)
                .ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString(), true);
            }

        }


        public IEnumerable<DropDownListItems> GetEntityDropDownListItems_ByGenderId(
          int genderId)
        {
            var selectListItem = this._context.Member
                .Include(a=> a.Person)
                .Where(a => a.IsActive && a.Person.GenderId == genderId)
                .ToDropDownListItem();

            return selectListItem;
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByAwardGenderId(
         int awardId)
        {
            var award = _context.Award.Include(a => a.Gender).FirstOrDefault(a => a.Id == awardId);

            if (award == null)
                throw new ArgumentNullException("Award does not exist");
          

            var selectListItem = this._context.Member
                .Include(a => a.Person.Gender)
                .WhereIf(award.Gender != null,a => a.Person.GenderId == award.GenderId)
                .ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString());

            return selectListItem;

        }

        public IEnumerable<SelectListItem> GetSelectListItem(bool excludeDefault)
        {

            var selectListItem = this._context.Member
                .Include(a => a.Person.Gender)
                .ToSelectListItem(a => a.Person.FullName, x => x.Id.ToString(),excludeDefault);

            return selectListItem;
        }
        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(MemberViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var entity = new Member();
            viewModel.Address.SessionUserId = viewModel.SessionUserId;

            try
            {

                saveResult = await SaveAddress(viewModel);

                if (saveResult.IsSuccess)
                {
                    if (_context.Member.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.Member
                            .IgnoreQueryFilters()
                            .Include(a => a.Person)
                             .Include(a => a.MemberMappings)
                            .FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                        entity = viewModel.ToEntity(entity);
                        _personBL.UpdateEntity(viewModel, entity);
                        _context.Member.Update(entity);
                    }
                    else
                    {

                        entity = viewModel.ToEntity(entity);
                        _context.Member.Add(entity);
                    }
                    await _context.SaveChangesAsync();

                    saveResult = await _memberMappingBL.SaveEntityList(viewModel, entity);

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

        private async Task<SaveResult> SaveAddress(MemberViewModel viewModel)
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


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Member.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Member.Remove(entity);
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