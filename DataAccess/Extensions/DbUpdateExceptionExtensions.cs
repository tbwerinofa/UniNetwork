using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public static class DbUpdateExceptionExtensions
    {
        public static Dictionary<bool, string> GetValidateEntityResults(this Dictionary<bool, string> dictionary, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                dictionary.Add(false, message);
            }
            return dictionary;
        }

        public static Dictionary<int, string> GetValidateEntityResults(this Dictionary<int, string> dictionary, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                dictionary.Add(0, message);
            }
            return dictionary;
        }
        public static int GetSqlerrorNo(this DbUpdateException dbUpdateEx) //It returns sqlException Number
        {

            if (dbUpdateEx != null)
            {
                if (dbUpdateEx != null
                        && dbUpdateEx.InnerException != null)
                {
                    //SqlException sqlException = dbUpdateEx.InnerException as SqlException;
                    //if (sqlException != null)
                    //{
                    //    return sqlException.Number;
                    //}

                    return 0;
                }
            }
            return 0;
        }
    }
}
