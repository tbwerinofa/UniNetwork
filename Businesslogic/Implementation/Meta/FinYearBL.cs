using BusinessLogic.Interface;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class FinYearBL : IFinYearBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public FinYearBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read
        public IEnumerable<SelectListItem> GetLatestFinYearSelectItem()
        {
            var currentYear = DateTime.Now.Year;
            return _context.FinYear.Where(a => a.Name <= currentYear)
                .ToSelectListItem(a => a.Name.ToString(), x => x.Id.ToString(), true)
                .OrderByDescending(a => a.Text);

        }


        public IEnumerable<SelectListItem> GetSelectListItem(bool excludeDefaultItem = false, bool includeUpcomingYear = false)
        {

            int maxYear = DateTime.Now.Year;
            if (includeUpcomingYear && DateTime.Now.Month >= 11)
            {
                maxYear = maxYear + 1;
            }
            return _context.FinYear
            .Where(a => a.Name <= maxYear)
            .ToSelectListItem(x => x.Name.ToString(), x => x.Id.ToString(), excludeDefaultItem)
            .OrderByDescending(e => e.Text);
        }


        public int GetCurrentFinYearId()
        {
            int currentYear = DateTime.Now.Year;
            int finYearID = 0;

            if (_context.FinYear.Any(a => a.Name == currentYear))
            {
                finYearID = _context.FinYear.Where(a => a.Name == currentYear).First().Id;
            }
            else
            {
                throw new ArgumentNullException("Current Year does not exist");
            }

            return finYearID;
        }
        #endregion

        #endregion
    }
}
