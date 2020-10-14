using BusinessObject.ResultSet;
using BusinessObject.ViewModel;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class StoredProcTransform
    {


        public static IEnumerable<TrophyWinner_ResultSetModel> ToListViewModel(
           this IEnumerable<DataAccess.TrophyWinner_ResultSet> entity)
        {
            return entity.Select(a =>
                        new TrophyWinner_ResultSetModel
                        {
                            TrophyId = a.TrophyId,
                            Trophy = a.Trophy,
                            Description = a.Description,
                            DocumentId = a.DocumentId ,
                            Award = a.Award,
                            Gender = a.Gender,
                            MemberNo = a.MemberNo,
                            FullName = a.FullName,
                            DocumentTypeId = a.DocumentTypeId,
                            DocumentData = a.DocumentData,
                            DocumentNameGuid = a.DocumentNameGuid,
                            DocumentName = a.DocumentName,
                            FinYear = a.FinYear,
                            Ordinal = a.Ordinal
                       });
        }


    }
}
