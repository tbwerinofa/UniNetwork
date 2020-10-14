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
using System.Text;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class MenuBL : IEntityViewLogic<MenuViewModel>, IMenuBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public MenuBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read
        public IEnumerable<MenuViewModel> GetEntityListBy_RoleList(
            IEnumerable<string> roleList,
            ApplicationUser sessionUser,
            string menuArea)
        {
            var resultSet = _context.ApplicationRoleMenu
                .Where(a=> a.IsActive && roleList.Contains(a.ApplicationRole.Name))
                .ToListViewModel(menuArea, sessionUser.UserName, sessionUser.Id);

    

            return resultSet;
        }


        public ResultSetPage<MenuViewModel> GetEntityListBySearchParams(
   GridLoadParam paramList)
        {

            var propertyInfo = typeof(MenuViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Menu
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<MenuViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new MenuViewModel();

            if (Id > 0)
            {
                var entity = await _context.Menu
                 
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }
        #endregion


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(MenuViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

  
            try
            {
                var entity = new Menu();
                if (viewModel.Id != 0)
                {
                    if (_context.Menu.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.Menu.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.Menu.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);
                    _context.Menu.Add(entity);
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
                var entity = await _context.Menu.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Menu.Remove(entity);
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