using BusinessLogic.Interface;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class FinYearCycleBL :  IFinYearCycleBL
    {
        #region Global Fields

        protected readonly SqlServerApplicationDbContext _context;

        #endregion

        #region Constructors
        public FinYearCycleBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        #region Read

        public IEnumerable<SelectListItem> GetSelectListItem(int? parentId)
        {

            return _context.FinYearCycle
                .Include(a => a.Cycle)
            .WhereIf(!parentId.HasValue, a => a.FinYear.Name <= DateTime.Now.Year)
            .WhereIf(parentId.HasValue, a => a.FinYearId == parentId)
            .OrderBy(e => e.Cycle.Name)
            .ToSelectListItem(x => x.Cycle.Name.ToString(), x => x.Id.ToString());
        }

        public IQueryable<FinYearCycle> GetLatestEntityList()
        {

            var endYear = DateTime.Now.AddMonths(6).Year;
            var startYear = DateTime.Now.Year;

            var years = _context.FinYearCycle
                .Include(a => a.Cycle)
                .Include(a => a.FinYear);

            return _context.FinYearCycle
                .Include(a => a.Cycle)
                .Include(a => a.FinYear)
                .Where(a => a.FinYear.StartDate.Year >= startYear && a.FinYear.EndDate.Year <= endYear)
            .OrderBy(e => e.Cycle.Name)
            .Take(2);

        }

        public FinYearCycle GetLatestEntity()
        {

            var startDate = DateTime.Now;


            var entity = _context.FinYearCycle
                .Include(a => a.Cycle)
            .Where(a => a.StartDate <= startDate && a.EndDate >= startDate)
            .OrderBy(e => e.Cycle.Name)
            .FirstOrDefault();

            if (entity == null)
                throw new ArgumentNullException(nameof(FinYearCycle));


            return entity;
        }

        #endregion


        #endregion
    }
}
