using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessLogic.Implementation
{
    public class FileUploader
    {

        Amazon.S3.AmazonS3Client S3Client = null;

        public FileUploader(string accessKeyId, string secretAccessKey, string serviceUrl)
        {
            Amazon.S3.AmazonS3Config s3Config = new Amazon.S3.AmazonS3Config();
            s3Config.ServiceURL = serviceUrl;

            this.S3Client = new Amazon.S3.AmazonS3Client(accessKeyId, secretAccessKey, s3Config);
        }


        public async System.Threading.Tasks.Task UploadFile(string filePath, string s3Bucket, string newFileName, bool deleteLocalFileOnSuccess, IFormFile fileUploaded)
        {
            //save in s3
            Amazon.S3.Model.PutObjectRequest s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest = new Amazon.S3.Model.PutObjectRequest();

            using (var ms = new MemoryStream())
            {
                fileUploaded.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                s3PutRequest.InputStream = ms;
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(fileUploaded.ContentDisposition);
                //document.Name = parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"');
            }


        
            //s3PutRequest.FilePath = filePath;
            s3PutRequest.BucketName = s3Bucket;
            s3PutRequest.CannedACL = Amazon.S3.S3CannedACL.PublicRead;

            //key - new file name
            if (!string.IsNullOrWhiteSpace(newFileName))
            {
                s3PutRequest.Key = newFileName;
            }

            s3PutRequest.Headers.ExpiresUtc = new DateTime(2020, 1, 1);

            try
            {
                Amazon.S3.Model.PutObjectResponse s3PutResponse = await this.S3Client.PutObjectAsync(s3PutRequest);

                if (deleteLocalFileOnSuccess)
                {
                    ////Delete local file
                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                }
            }
            catch (Exception ex)
            {
                //handle exceptions
            }

        }

        public async System.Threading.Tasks.Task UploadFileAsync(string filePath, string s3Bucket, string newFileName, bool deleteLocalFileOnSuccess, byte[] renderedBytes)
        {
            //save in s3
            Amazon.S3.Model.PutObjectRequest s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest = new Amazon.S3.Model.PutObjectRequest();

            s3PutRequest.InputStream = new MemoryStream(renderedBytes);
            //s3PutRequest.FilePath = filePath;
            s3PutRequest.BucketName = s3Bucket;
            s3PutRequest.CannedACL = Amazon.S3.S3CannedACL.PublicRead;

            //key - new file name
            if (!string.IsNullOrWhiteSpace(newFileName))
            {
                s3PutRequest.Key = newFileName;
            }

            s3PutRequest.Headers.ExpiresUtc = new DateTime(2025, 1, 1);

            try
            {
                Amazon.S3.Model.PutObjectResponse s3PutResponse = await this.S3Client.PutObjectAsync(s3PutRequest);

                if (deleteLocalFileOnSuccess)
                {
                    ////Delete local file
                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                }
            }
            catch (Exception ex)
            {
                //handle exceptions
            }

        }
    }
}

