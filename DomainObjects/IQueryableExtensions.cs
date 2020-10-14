using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DomainObjects
{
    public static class IQueryableExtensions
    {

        #region Fields
        private static readonly SelectListItem DefaultSelectListItem = new SelectListItem()
        {
            Value = "0",
            Text = "-- choose --"
        };

        private static readonly DropDownListItems DefaultItem = new DropDownListItems()
        {
            Value = 0,
            Text = "-- choose --"
        };

        #endregion


        #region Methods

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
    where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }


        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ?
                source.Where(predicate) :
                source;
        }

        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ?
                source.AsQueryable().Where(predicate) :
                source;
        }

        public static IEnumerable<SelectListItem> ToSelectListItem<TSource>(
                this IEnumerable<TSource> enumerable,
                Func<TSource, string> text,
                Func<TSource, string> value,
                bool excludeDefaultItem = false,
                IEnumerable<int> selected = null,
                bool includeDefaultItem = false,
                bool excludeSort = false)
        {
            var result = new List<SelectListItem>();
            string record = string.Empty;
            if (!excludeDefaultItem && enumerable.Count() != 1)
            {
                result.Add(DefaultSelectListItem);
            }
            else if (includeDefaultItem)
            {
                result.Add(DefaultSelectListItem);
            }

            foreach (TSource model in enumerable)
            {
                var selectItem = new SelectListItem
                {
                    Text = text(model),
                    Value = value(model)
                };

                if (selected != null)
                {
                    if (int.TryParse(selectItem.Value, out int selectedId))
                    {

                        selectItem.Selected = selected.Contains(int.Parse(selectItem.Value)) ? true : false;
                    }
                }

                result.Add(selectItem);
            }


            if (excludeSort)
                return result;
            else
                return result.OrderBy(a => a.Text);
        }
        
        public static IEnumerable<SelectListItem> Default_SelectListItem()
        {
            return new[] { DefaultSelectListItem };
        }


        public static IEnumerable<DropDownListItems> ToDropDownListItem<TSource>(
    this IEnumerable<TSource> enumerable,
    Func<TSource, string> text,
    Func<TSource, int> value,
    bool excludeDefaultItem = false,
    IEnumerable<int> selected = null,
    bool includeDefaultItem = false)
        {
            var result = new List<DropDownListItems>();
            string record = string.Empty;
            if (!excludeDefaultItem && enumerable.Count() != 1)
            {
                result.Add(DefaultItem);
            }
            else if (includeDefaultItem)
            {
                result.Add(DefaultItem);
            }

            foreach (TSource model in enumerable)
            {
                var selectItem = new DropDownListItems
                {
                    Text = text(model),
                    Value = value(model)
                };

                if (selected != null)
                {

                    selectItem.Selected = selected.Contains(selectItem.Value) ? true : false;

                }

                result.Add(selectItem);
            }

            return result.OrderBy(a => a.Text);
        }

    

        public static IEnumerable<DropDownListItems> Default_DropDownItem()
        {
            return new[] { DefaultItem };
        }


        public static ResultSetPage<T> ToResultSetPage<T>(
     this GridLoadParam param,
      System.Reflection.PropertyInfo propertyInfo,
      IEnumerable<T> entityList)
        {
            try
            {

       
            var recordCount = param.Take == -1 ? entityList.Count() : param.Take;

            return new ResultSetPage<T>(
           param.Skip,
           entityList.Count(),
           entityList.ToList()
          .OrderByWithDirection(x => propertyInfo.GetValue(x, null), param.IsDescending)
          .Skip(param.Skip)
          .Take(recordCount)
          .ToList());
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>
     (this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey>
            (this IQueryable<TSource> source,
             Expression<Func<TSource, TKey>> keySelector,
             bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }
        #endregion

    }
}
