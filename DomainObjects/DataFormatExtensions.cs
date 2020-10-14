using BusinessObject.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects
{
    public static class DataFormatExtensions
    {
        /// <summary>
        /// Transform parameters into GridloadParams class
        /// </summary>
        ///<param name="userID">Current UserID</param>
        ///<param name="skip">Records to skip</param>
        ///<param name="take">Record to return</param>
        ///<param name="isDescending">Sort direction</param>
        ///<param name="sortField">Sort Field</param>
        ///<param name="searchFilter">Term to filter records</param>
        ///<returns>GridLoadParam</returns>
        public static GridLoadParam ToGridLoadParam(this string userID,
            int skip,
            int take,
            bool isDescending,
            string sortField,
            string searchFilter)
        {
            var param = new GridLoadParam
            {
                SessionUserId = userID,
                Skip = skip,
                Take = take,
                IsDescending = isDescending,
                SortField = sortField,
            };
            return param;
        }
        public static string ToCustomShortDate(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd MMM yyyy}", value) : string.Empty;
        }
        public static string ToCustomShortDate(this DateTime value)
        {
            return string.Format("{0:dd MMM yyyy}", value);
        }

        public static string ToCustomPreciseShortDate(this DateTime value)
        {
            return string.Format("{0:dd/MM/yyyy}", value);
        }
        public static string ToCustomLongDateTime(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", value) : string.Empty;
        }
        public static string ToCustomLongDateTime(this DateTime value)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", value);
        }

        public static string ToCustomLongDate(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd MMMM yyyy}", value) : string.Empty;
        }
        public static string ToCustomLongDate(this DateTime value)
        {
            return string.Format("{0:dd MMMM yyyy}", value);
        }

        public static string ToMonetaryValue(this decimal? value)
        {
            return value.HasValue ? string.Format("R{0:###,###.00}", value) : string.Empty;
        }

        public static string ToMonetaryValue(this decimal value)
        {
            return string.Format("R{0:###,###.00}", value);
        }

        public static string ToYearMonthDate(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:MMMM}", currentDate);
        }

        public static string ToMonth(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:MMM}", currentDate);
        }

        public static string ToDayMonth(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:dd MMMM}", currentDate);
        }

        public static String Number2String(this int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        public static SaveResult ToSaveResult(
    this Dictionary<bool, string> data)
        {
            var saveResult = new SaveResult();

            foreach (var item in data)
            {
                if (item.Key)
                {
                    saveResult.IsSuccess = item.Key;
                }
                else
                {
                    saveResult.Message = item.Value;
                }

            }

            return saveResult;
        }

        public static SaveResult ToSaveResult(
         this Dictionary<int, string> data)
        {
            var saveResult = new SaveResult();

            foreach (var item in data)
            {
                if (item.Key > 0)
                {
                    saveResult.Id = item.Key;
                    saveResult.IsSuccess = true;
                }
                else
                {
                    saveResult.Message = item.Value;
                }

            }

            return saveResult;
        }

        public static List<Tuple<string, string>> ToReportParam(this
    string finYear,
    string param)
        {


            var reportParams = new List<Tuple<string, string>>
            {
                Tuple.Create(param.ToString(), finYear)
            };

            return reportParams;

        }
    }
}
