using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class QuoteTransform
    {
        /// <summary>
        /// Convert Quote Object into Quote Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Quote</param>
        ///<returns>IEnumerable Quote</returns>
        ///
        public static IEnumerable<QuoteViewModel> ToListViewModel(
            this IQueryable<DataAccess.Quote> entity)
        {
            var quoteDetails = entity.ToList().SelectMany(a => a.QuoteDetails);

            return quoteDetails.Select(a =>
                        new QuoteViewModel
                        {
                            Id = a.QuoteId,
                            QuoteNo = a.Quote.QuoteNo,
                            QuoteStatusId = a.Quote.QuoteStatusId,
                            PaymentDate = a.Quote.PaymentDate,
                            PaymentReference = a.Quote.PaymentReference,
                            QuoteStatus = a.Quote.QuoteStatus.Name,
                            SubscriptionCount = a.Quote.QuoteDetails.Count,
                            UserId = a.Quote.QuoteUserId,
                            Quantity = a.Quantity,
                            FinYearId = a.Quote.FinYearId,
                            FinYear = a.Quote.FinYear.Name,
                            Amount = a.Quantity * a.SubscriptionTypeRuleAudit.AmountRand,
                            SessionUserId = a.CreatedUserId,
                            CreatedTimestamp = a.CreatedTimestamp.ToCustomLongDate(),
                            FullName = a.Quote.QuoteUser != null
                            ?(a.Quote.QuoteUser.FirstName + " " + a.Quote.QuoteUser.Surname) 
                            :"",
                            LastUpdateBy = a.Quote.QuoteUser != null
                            ? (a.Quote.QuoteUser.FirstName + " " + a.Quote.QuoteUser.Surname)
                            : "",
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        }).GroupBy(a => new
                        { a.QuoteNo, a.QuoteStatus, a.CreatedTimestamp, a.Id,a.PaymentDate,a.PaymentReference,a.FullName,a.UserCode,a.FinYearId ,a.FinYear,a.LastUpdateBy,a.LastUpdate ,a.LastUpdatedUser})
                       .Select(g => new QuoteViewModel
                       {
                           Id = g.Key.Id,
                           QuoteNo = g.Key.QuoteNo,
                           QuoteStatus = g.Key.QuoteStatus,
                           SubscriptionCount = g.Count(),
                           Quantity = g.Sum(a => a.Quantity),
                           Amount = g.Sum(a => a.Amount),
                           CreatedTimestamp = g.Key.CreatedTimestamp,
                           PaymentDate = g.Key.PaymentDate,
                           PaymentReference = g.Key.PaymentReference,
                           FullName = g.Key.FullName,
                           UserCode = g.Key.UserCode,
                           FinYearId = g.Key.FinYearId,
                           FinYear = g.Key.FinYear,
                           LastUpdateBy = g.Key.LastUpdateBy,
                            LastUpdate = g.Key.LastUpdate,
                             LastUpdatedUser = g.Key.LastUpdatedUser
                       }).AsEnumerable();
        }

        /// <summary>
        /// Convert Quote Object into Quote Entity
        /// </summary>
        ///<param name="model">Quote</param>
        ///<param name="QuoteEntity">DataAccess.Quote</param>
        ///<returns>DataAccess.Quote</returns>
        public static DataAccess.Quote ToEntity(this QuoteViewModel model,
            DataAccess.Quote entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.QuoteStatusId = model.QuoteStatusId;
            entity.QuoteNo = model.QuoteNo;
            entity.QuoteUserId = model.UserId;
            entity.PaymentDate = model.PaymentDate;
            entity.FinYearId = model.FinYearId;
            entity.PaymentReference = model.PaymentReference;

            return entity;
        }

        /// <summary>
        /// Convert Quote Entity  into Quote Object
        /// </summary>
        ///<param name="model">QuoteViewModel</param>
        ///<param name="QuoteEntity">DataAccess.Quote</param>
        ///<returns>QuoteViewModel</returns>
        public static QuoteViewModel ToViewModel(
         this DataAccess.Quote entity,
         QuoteViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;

            model.QuoteStatusId = entity.QuoteStatusId;
            model.QuoteStatus = entity.QuoteStatus.Name;
            model.QuoteStatusRef = entity.QuoteStatus.Discriminator;
            model.QuoteNo = entity.QuoteNo;
            model.UserId = entity.QuoteUserId;
            model.PaymentDate = entity.PaymentDate;
            model.PaymentReference = entity.PaymentReference;
            model.FinYearId = entity.FinYearId;
            model.QuoteDetails = entity.ToListViewModel();
            model.RequiresPayment = !entity.QuoteDetails.Any(a => a.Subscriptions.Any());

            //if(entity.QuoteDetails.Any(a=> a.QuoteDetailOrganisations.Any()))
            //{
            //    var withOrg = entity.QuoteDetails.SelectMany(a => a.QuoteDetailOrganisations);
            //    model.TotalAmount = withOrg.Sum(a => a.Organisation.Sector.SectorPricings.Where(c => c.FinYearId == entity.FinYearId && c.IsFinalised).Sum(d=> d.AmountRand * a.QuoteDetail.Quantity));
            //    model.TotalAmountExVat = withOrg.Sum(a => a.Organisation.Sector.SectorPricings.Where(c => c.FinYearId == entity.FinYearId && c.IsFinalised).Sum(d => d.AmountRandExVat * a.QuoteDetail.Quantity))??0;
            //    model.TotalVatAmount = withOrg.Sum(a => a.Organisation.Sector.SectorPricings.Where(c => c.FinYearId == entity.FinYearId && c.IsFinalised).Sum(d => d.VatRand * a.QuoteDetail.Quantity)) ?? 0;
            //}


            if (entity.QuoteDetails.Any())
            {
                var withoutOrg = entity.QuoteDetails;
                model.TotalAmount = withoutOrg.Sum(a => a.SubscriptionTypeRuleAudit.AmountRand * a.Quantity);
                model.TotalAmountRand = withoutOrg.Sum(a => a.SubscriptionTypeRuleAudit.AmountRand * a.Quantity).ToMonetaryValue();
            }
            else
            { 
                if(entity.OrderDetails.Any())
                {
                    var data = entity.OrderDetails
                   .ToList()
                   .Select(a => new ShoppingCartViewModel
                   {
                       CartTotal = a.ProductSize.Product.Price * a.Quantity,
                       VAT = (a.ProductSize.Product.Vat ?? 0) * a.Quantity,
                       TotalExcludingVAT = ((a.ProductSize.Product.Price) * a.Quantity) - (a.ProductSize.Product.Vat ?? 0 * a.Quantity)
                   });
                    model.TotalAmount = data.Sum(a => a.CartTotal);
                    model.TotalVatAmount = data.Sum(a => a.VAT);
                    model.TotalAmountExVat = data.Sum(a => a.TotalExcludingVAT);
          
                }
            }
            model.FullName = entity.QuoteUser.FullName;

            return model;
        }

        /// <summary>
        /// Convert Quote Entity  into Quote Object
        /// </summary>
        ///<param name="model">QuoteViewModel</param>
        ///<param name="QuoteEntity">DataAccess.Quote</param>
        ///<returns>QuoteViewModel</returns>
        public static PayFastViewModel ToPayFastViewModel(
         this DataAccess.Quote entity,
         decimal totalAmount,
         SaveResult saveResult)
        {

            var payFastVieModel = new PayFastViewModel
            {
                SaveResult = saveResult,
                name_first = entity.QuoteUser.FirstName,
                name_last = entity.QuoteUser.Surname,
                email_address = entity.QuoteUser.Email,
                cell_number = entity.QuoteUser.ContactNo,
                m_payment_Id = entity.QuoteNo,
                amount = totalAmount,
                item_name = "Subscription",
                item_description = string.Empty,
                custom_int1 = entity.Id,
                email_confirmation = 1,
                confirmation_address = entity.QuoteUser.Email,
                signature = string.Empty
            };

            return payFastVieModel;
        }

        public static IEnumerable<QuoteViewModel> ToQueryListViewModel(
         this IQueryable<DataAccess.Quote> entity)
        {

            return entity.Select(a =>
                        new QuoteViewModel
                        {
                            Id = a.Id,
                            QuoteNo = a.QuoteNo,
                            QuoteStatusId = a.QuoteStatusId,
                            PaymentDate = a.PaymentDate,
                            PaymentReference = a.PaymentReference,
                            QuoteStatus = a.QuoteStatus.Name,
                            SubscriptionCount = a.QuoteDetails.Count,
                        });
        }
    }
}
