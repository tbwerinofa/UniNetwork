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
    public class CalendarBL : IEntityViewLogic<CalendarViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IFinYearBL _finYearBL;
        protected readonly IModeratorBL _moderatorBL;

        #region Constructors
        public CalendarBL(SqlServerApplicationDbContext context, IFinYearBL finYearBL, IModeratorBL moderatorBL)
        {
            _context = context;
            _finYearBL = finYearBL;
            _moderatorBL = moderatorBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<CalendarViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(CalendarViewModel).GetProperty(paramList.SortField);

            var data = _context.Calendar;

            var resultSet = _context.Calendar
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Event.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<CalendarViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new CalendarViewModel {
                ScheduleDate = DateTime.Now
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Calendar
                    .IgnoreQueryFilters()
                    .Include(c => c.Event)
                    .Include(c => c.FinYear)
                    .Include(c => c.Venue)
                    .Include(c => c.TimeTrials)
                    .Include(c => c.Moderators)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(CalendarViewModel model)
        {
            model.FinYears = _finYearBL.GetLatestFinYearSelectItem();
            model.Venues = _context.Venue.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Events = _context.Event.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Moderators = _context.Member.Include(a=>a.Person).ToDropDownListItem(a => a.Person.FullName, x => x.Id, excludeDefaultItem: true);
        }



        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Calendar
                .ToSelectListItem(x => x.Event.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(CalendarViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Calendar();
         
            try
                {
                bool isTimeTraininig = _context.Event.Include(a => a.EventType).Where(a=> a.Id == viewModel.EventId).Any(b => b.EventType.Discriminator == EventTypeDiscr.Training);


                if(isTimeTraininig && viewModel.ModeratorIds != null)
                {
                    if(viewModel.ModeratorIds.Count() >4)
                    {
                        saveResult.Message = "Can not select more than four moderators";
                        return saveResult;
                    }
                }
                    if (viewModel.Id != 0)
                    {
                        if (_context.Calendar.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Calendar
                            .Include(a => a.TimeTrials)
                             .Include(a => a.Moderators)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);

                    #region time trial
                    if (isTimeTraininig)
                    {
                        MapToTimeTrial(entity, viewModel);
                    }
                    else
                    {
                        if (entity.TimeTrials.Any())
                        {
                            _context.TimeTrial.RemoveRange(entity.TimeTrials.ToList());
                        }
                    }
                    #endregion

                    _context.Calendar.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);

                    if (isTimeTraininig)
                    {
                        MapToTimeTrial(entity, viewModel);
                    }

                    _context.Calendar.Add(entity);
                    }

                    await _context.SaveChangesAsync();


                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await _moderatorBL.SaveEntityList(viewModel, entity);
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

                    saveResult.Message = CrudError.SaveErrorMsg;
                }


                return saveResult;
            }

        private void MapToTimeTrial(Calendar dbentity, CalendarViewModel model)
        {
            RaceType raceTypeEntity = _context.RaceType.FirstOrDefault(a => a.Name == "Training");

            if (dbentity.TimeTrials.Any())
            {
                dbentity.TimeTrials
                    .ToList()
                    .ForEach(a =>
                    {
                        a.RaceTypeId = raceTypeEntity.Id;
                        a.UpdatedTimestamp = DateTime.Now;
                        a.UpdatedUserId = model.SessionUserId;
                    }
                    );
            }
            else
            {
                dbentity.TimeTrials.Add(new TimeTrial
                {
                    RaceTypeId = raceTypeEntity.Id,
                    CalendarId = dbentity.Id,
                    CreatedUserId = model.SessionUserId
                });
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
                var entity = await _context.Calendar.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Calendar.Remove(entity);
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
