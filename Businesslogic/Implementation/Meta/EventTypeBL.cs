﻿using BusinessLogic.Interface;
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
    public class EventTypeBL : IEntityViewLogic<EventTypeViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public EventTypeBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<EventTypeViewModel> GetEntityListBySearchParams(
   GridLoadParam paramList)
        {

            var sortfield = paramList.SortField ?? "Name";
            var propertyInfo = typeof(EventTypeViewModel).GetProperty(sortfield);

            var resultSet = _context.EventType
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<EventTypeViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new EventTypeViewModel();

            if (Id > 0)
            {
                var entity = await _context.EventType.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }
            return viewModel;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.EventType
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(EventTypeViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                
                try
                {

                var entity = new EventType();
                if (viewModel.Id != 0)
                    {
                        if (_context.EventType.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.EventType.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.EventType.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.EventType.Add(entity);
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
                var entity = await _context.EventType.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.EventType.Remove(entity);
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
        #endregion
    }
}
