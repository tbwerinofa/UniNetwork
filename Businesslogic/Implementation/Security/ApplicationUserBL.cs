using BusinessLogic.Interface;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class ApplicationUserBL : IApplicationUserBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public ApplicationUserBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public ResultSetPage<UserViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(UserViewModel).GetProperty(paramList.SortField);

            var resultSet = this._context.ApplicationUser
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a =>
                 a.FullName.ToLower().Contains(paramList.SearchTerm) || a.Email.ToLower().Contains(paramList.SearchTerm) || a.UserName.ToLower().Contains(paramList.SearchTerm)
                )
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }

        public async Task<UserViewModel> GetEntityById(
           string Id)
        {

            var viewModel = new UserViewModel();

            if (!String.IsNullOrEmpty(Id))
            {
                var entity = await _context.ApplicationUser
                     .Include(a => a.UserRegions)
                     .Include(a => a.UserRegions).ThenInclude(b => b.Region)
                    .FirstOrDefaultAsync(a=>a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }
        #endregion

    }
}