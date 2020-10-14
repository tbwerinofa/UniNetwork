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
    public class ArticleBL : IEntityViewLogic<ArticleViewModel>//, IArticleBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        IFinYearBL _finYearBL;

        #region Constructors
        public ArticleBL(SqlServerApplicationDbContext context,
             IFinYearBL finYearBL)
        {
            _context = context;
            _finYearBL = finYearBL;
        }

        #endregion
        #region Methods

        #region Read
        public async Task<ArticleViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ArticleViewModel
            {

                FinYears = _finYearBL.GetLatestFinYearSelectItem(),
                CalendarMonths = _context.CalendarMonth.OrderBy(a=>a.Ordinal).ToSelectListItem(a => a.Name, x => x.Id.ToString(), excludeSort: false),
                PublishDate = DateTime.Now
        };

            if (Id > 0)
            {
                var entity = await _context.Article
                    .IgnoreQueryFilters()
                    .Include(a=>a.Newsletters)
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);

            }

    


            return viewModel;
        }

        public IEnumerable<ArticleViewModel> GetArticles()
        {

            return _context.Article
                .ToListViewModel();
        }

        public ResultSetPage<ArticleViewModel> GetEntityListBySearchParams(
    GridLoadParam param)
        {

            var propertyInfo = typeof(ArticleViewModel).GetProperty(param.SortField);

            var resultSet = _context.Article
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(param.SearchTerm), a => a.Name.Contains(param.SearchTerm))
                .ToListViewModel();

            return param.ToResultSetPage(propertyInfo, resultSet);

        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(ArticleViewModel viewModel)
        {
            try
            {


                SaveResult saveResult = new SaveResult();

                var entity = new Article();

                if (viewModel.Id != 0)
                {
                    if (_context.Article.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                    {
                        entity = await _context.Article.IgnoreQueryFilters().Include(a=>a.Newsletters).FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                    }
                    entity = viewModel.ToEntity(entity);

                    if (viewModel.IssueNo.HasValue)
                    {
                        MapToAudit(entity, viewModel);
                    }
                    else
                    {
                        if (entity.Newsletters.Any())
                        {
                         _context.Newsletter.RemoveRange(entity.Newsletters.ToList());
                        }
                    }

                    _context.Article.Update(entity);
                }
                else
                {
                    entity = viewModel.ToEntity(entity);

                    if (viewModel.IssueNo.HasValue)
                    {
                        MapToAudit(entity, viewModel);
                    }
                    _context.Article.Add(entity);
                  
                }

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }

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

        private void MapToAudit(Article dbentity, ArticleViewModel model)
        {


            if (dbentity.Newsletters.Any())
            {
                dbentity.Newsletters
                    .ToList()
                    .ForEach(a =>
                    {
                        a.IsActive = false;
                        a.IssueNo = model.IssueNo??0;
                        a.UpdatedTimestamp = DateTime.Now;
                        a.UpdatedUserId = model.SessionUserId;
                    }
                    );
            }
            else
            {
                dbentity.Newsletters.Add(new Newsletter
                {
                    IsActive = false,
                    IssueNo = model.IssueNo??0,
                    ArticleId = dbentity.Id,
                    CreatedUserId = model.SessionUserId
                });
            }

        }

        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();

            try
            {
                var entity = await _context.Article.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);
                _context.Article.Remove(entity);
            await _context.SaveChangesAsync();

            resultSet.IsSuccess = true;
        }
            catch (DbUpdateException ex)
            {
                resultSet.Message = "Error deleting record";

            }
            catch (Exception ex)
            {

                resultSet.Message = "Error deleting record";
            }
            return resultSet;

        }
        #endregion

        #endregion
    }
}
