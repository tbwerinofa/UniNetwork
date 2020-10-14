using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ErrorLogging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainObjects
{
    public static class AWSS3Files
    {
        public static async System.Threading.Tasks.Task<bool> UploadS3FileAsync(this string fileName, IFormFile formFile,string _awsAccessKey,string _awsSecretKey,string _bucketName)
        {
            IAmazonS3 client;
            var s3Client = RegionEndpoint.USEast1;

            try
            {

                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);

                    using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, s3Client))
                    {
                        var request = new PutObjectRequest()
                        {
                            BucketName = _bucketName,
                            CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                            Key = string.Format(fileName),
                            InputStream = ms//SEND THE FILE STREAM
                        };

                        await client.PutObjectAsync(request);
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Log("Upload Documents failure", "S3 File upload Extension Method", exception);
                return false;
            }

            return true;
        }

        public static async System.Threading.Tasks.Task<bool> Delete(this string fileNameGuid, string _awsAccessKey, string _awsSecretKey, string _bucketName)
        {
            IAmazonS3 client;
            var s3Client = RegionEndpoint.USEast1;

            try
            {
                using (client = new Amazon.S3.AmazonS3Client(_awsAccessKey, _awsSecretKey, s3Client))
                {
                    DeleteObjectRequest request = new DeleteObjectRequest()
                    {
                        BucketName = _bucketName,
                        Key = fileNameGuid,
                    };

                    await client.DeleteObjectAsync(request);
                    client.Dispose();
                }
            }
            catch (Exception exception)
            {
                Logging.Log("Upload Documents failure", "S3 File upload Extension Method", exception);
                return false;
            }

            return true;
        }
    }
}
