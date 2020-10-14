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
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class FeaturedImageBL : IFeaturedImageBL
    {
        #region Global Fields
        protected readonly SqlServerApplicationDbContext _context;
        #endregion

        #region Constructors
        public FeaturedImageBL(SqlServerApplicationDbContext context)
        {
            _context = context;

        }
        #endregion
        #region Create/Update

        public async Task<SaveResult> SaveEntity(ProductImageViewModel viewModel)
        {


            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();


            try
            {
                var featuredImageCollection = this._context.FeaturedImage.Where(a => a.ProductImageId == viewModel.Id).ToList();

                if (featuredImageCollection != null)
                {
                    if (viewModel.FeaturedCategoryIds == null)
                    {
                        this._context.FeaturedImage.RemoveRange(featuredImageCollection);
                    }
                    else
                    {
                        var nonExistantEntityList = featuredImageCollection.Where(a => !viewModel.FeaturedCategoryIds.Contains(a.FeaturedCategoryId));
                        this._context.FeaturedImage.RemoveRange(nonExistantEntityList);

                    }
                }

                if (viewModel.FeaturedCategoryIds != null)
                {

                    foreach (var item in viewModel.FeaturedCategoryIds)
                    {
                        var featuredImageObj = this._context.FeaturedImage
                        .Where(a => a.FeaturedCategoryId == item
                                && a.ProductImageId == viewModel.Id)
                        .FirstOrDefault();

                        if (featuredImageObj == null)
                        {
                            featuredImageObj = new DataAccess.FeaturedImage();
                            featuredImageObj.ProductImageId = viewModel.Id;
                            featuredImageObj.FeaturedCategoryId = item;
                            featuredImageObj.CreatedUserId = viewModel.SessionUserId;

                            this._context.FeaturedImage.Add(featuredImageObj);
                        }
                        else
                        {
                            featuredImageObj.UpdatedTimestamp = DateTime.Now;
                            featuredImageObj.UpdatedUserId = viewModel.SessionUserId;
                            this._context.FeaturedImage.Update(featuredImageObj);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                saveResult.IsSuccess = true;
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

        #endregion

    }
}
