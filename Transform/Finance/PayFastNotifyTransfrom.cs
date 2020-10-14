using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class PayFastNotifyTransform
	{
		/// <summary>
		/// Convert PayFastNotify Object into PayFastNotify Entity
		/// </summary>
		///<param name="entity">IQueryable DataAccess.PayFastNotify</param>
		///<returns>IEnumerable PayFastNotify</returns>
		///
		public static IEnumerable<PayFastNotifyViewModel> ToListViewModel(
			this IQueryable<DataAccess.PayFastNotify> entity)
		{
            return entity.ToList().Select(a =>
                        new PayFastNotifyViewModel
                        {
                            Id = a.Id,
                            m_payment_id = a.M_payment_id,
                            pf_payment_id = a.Pf_payment_id,
                            payment_status = a.Payment_status,
                            item_name = a.Item_name,
                            item_description = a.Item_description,
                            amount_gross = a.Amount_gross,
                            amount_fee = a.Amount_fee,
                            amount_net = a.Amount_net,
                            custom_int1 = a.Custom_int1,
                            custom_str1 = a.Custom_str1,
                            name_first = a.Name_first,
                            name_last = a.Name_last,
                            email_address = a.Email_address,
                            fullname = a.Name_first + " " + a.Name_last,
                            UpdatedTimestamp = a.UpdatedTimestamp.ToCustomLongDateTime(),
                            CreatedTimestamp = a.CreatedTimestamp.ToCustomLongDateTime()
						});
		}

		/// <summary>
		/// Convert PayFastNotify Object into PayFastNotify Entity
		/// </summary>
		///<param name="model">PayFastNotify</param>
		///<param name="PayFastNotifyEntity">DataAccess.PayFastNotify</param>
		///<returns>DataAccess.PayFastNotify</returns>
		public static DataAccess.PayFastNotify ToEntity(this PayFastNotifyViewModel model,
            DataAccess.PayFastNotify entity)
		{
			if (entity.Id != 0)
			{
				entity.UpdatedTimestamp = DateTime.Now;
			}

                entity.M_payment_id = model.m_payment_id;
                entity.Pf_payment_id = model.pf_payment_id;
                entity.Payment_status = model.payment_status;
                entity.Item_name = model.item_name;
                entity.Item_description = model.item_description;
                entity.Amount_gross = model.amount_gross;
                entity.Amount_fee = model.amount_fee;
                entity.Amount_net = model.amount_net;
                entity.Custom_int1 = model.custom_int1;
                entity.Custom_str1 = model.custom_str1;
                entity.Name_first = model.name_first;
                entity.Name_last = model.name_last;
                entity.Email_address = model.email_address;

			return entity;
		}

		/// <summary>
		/// Convert PayFastNotify Entity  into PayFastNotify Object
		/// </summary>
		///<param name="model">PayFastNotifyViewModel</param>
		///<param name="PayFastNotifyEntity">DataAccess.PayFastNotify</param>
		///<returns>PayFastNotifyViewModel</returns>
		public static PayFastNotifyViewModel ToViewModel(
		 this DataAccess.PayFastNotify entity,
		 PayFastNotifyViewModel model)
		{


			model.Id = entity.Id;
            model.m_payment_id = entity.M_payment_id;
            model.pf_payment_id = entity.Pf_payment_id;
            model.payment_status = entity.Payment_status;
            model.item_name = entity.Item_name;
            model.item_description = entity.Item_description;
            model.amount_gross = entity.Amount_gross;
            model.amount_fee = entity.Amount_fee;
            model.amount_net = entity.Amount_net;
            model.custom_int1 = entity.Custom_int1;
            model.custom_str1 = entity.Custom_str1;
            model.name_first = entity.Name_first;
            model.name_last = entity.Name_last;
            model.email_address = entity.Email_address;
            return model;
		}
	}
}
