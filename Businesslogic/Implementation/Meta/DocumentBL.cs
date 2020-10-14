using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class DocumentBL : IDocumentBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected IFeaturedImageBL _featuredImage;
        private IConfiguration _configuration { get; }
        #region Constructors
        public DocumentBL(SqlServerApplicationDbContext context,
            IFeaturedImageBL featuredImage,
            IConfiguration configuration)
        {
            _context = context;
            this._configuration = configuration;
        }
        #endregion



        #region Create/Update

        public async Task UploadDocument(IFormFile formFile, Document entity, string sessionUserId)
        {
            try
            {

                var awsSection = _configuration.GetSection("AWSSettings");

                AWSObject awsconfigurations = new AWSObject
                {
                    AWSAccessKey = awsSection["AWSAccessKey"],
                    AWSSecretKey = awsSection["AWSSecretKey"],
                    BucketName = awsSection["BucketName"],
                    SessionUserId = sessionUserId
                };
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition);

                var document = new DocumentViewModel
                {
                    SessionUserId = awsconfigurations.SessionUserId,
                    DocumentTypeId = entity.DocumentTypeId,
                    DocumentNameGuid = Guid.NewGuid().ToString() + Path.GetExtension(parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"')),
                    Name = parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"')
                };

                await document.DocumentNameGuid.UploadS3FileAsync(formFile, awsconfigurations.AWSAccessKey, awsconfigurations.AWSSecretKey, awsconfigurations.BucketName);

                document.ToEntity(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task UploadDocument(IFormFile formFile,Document entity,string sessionUserId, string documentTypeDiscr)
        {
            try
            {

                var awsSection = _configuration.GetSection("AWSSettings");

                AWSObject awsconfigurations = new AWSObject
                {
                    AWSAccessKey = awsSection["AWSAccessKey"],
                    AWSSecretKey = awsSection["AWSSecretKey"],
                    BucketName = awsSection["BucketName"],
                    SessionUserId = sessionUserId
                };

                var docType = await _context.DocumentType.FirstOrDefaultAsync(a => a.Discriminator == documentTypeDiscr);

                if (docType == null)
                {
                    throw new ArgumentNullException(nameof(DocumentType));
                }

                var document = new DocumentViewModel
                {
                    SessionUserId = awsconfigurations.SessionUserId,
                    DocumentTypeId = docType.Id,
                    DocumentNameGuid = Guid.NewGuid().ToString()
                };


                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition);
                document.Name = parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"');

                await document.DocumentNameGuid.UploadS3FileAsync(formFile, awsconfigurations.AWSAccessKey, awsconfigurations.AWSSecretKey, awsconfigurations.BucketName);

                document.ToEntity(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task DeleteEntity(int documentId)
        {
            try
            {

                var awsSection = _configuration.GetSection("AWSSettings");

                AWSObject awsconfigurations = new AWSObject
                {
                    AWSAccessKey = awsSection["AWSAccessKey"],
                    AWSSecretKey = awsSection["AWSSecretKey"],
                    BucketName = awsSection["BucketName"],
                };


                var entity = await _context.Document.FirstOrDefaultAsync(a => a.Id == documentId);

                _context.Document.Remove(entity);
                await _context.SaveChangesAsync(); 

                await entity.DocumentNameGuid.Delete(awsconfigurations.AWSAccessKey, awsconfigurations.AWSSecretKey, awsconfigurations.BucketName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
