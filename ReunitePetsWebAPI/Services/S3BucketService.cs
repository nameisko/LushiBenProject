using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public static class S3BucketService
    {
        static AmazonS3Client s3Client = GetAmazonS3Client();
        static readonly string bucketName = "lushibenbucketlab3";

        public static BasicAWSCredentials GetAWSCredentials()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            string awsAccessKey = builder.Build().GetSection("AWSCredentials").GetSection("AccessKeyID").Value;
            string awsSecretKey = builder.Build().GetSection("AWSCredentials").GetSection("SecretAccessKey").Value;

            return new BasicAWSCredentials(awsAccessKey, awsSecretKey);
        }

        public static AmazonS3Client GetAmazonS3Client()
        {
            s3Client = new AmazonS3Client(GetAWSCredentials(), Amazon.RegionEndpoint.USEast1);

            return s3Client;
        }

        public static async Task<string> UploadImage(IFormFile image)
        {
            byte[] bytes = new Byte[image.Length];

            MemoryStream stream = new MemoryStream();

            await image.CopyToAsync(stream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = image.FileName,
                InputStream = stream
            };
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);
                return $"https:\\lushibenbucketlab3.s3.amazonaws.com\\{image.FileName}";
            }
            catch(Exception ex){ Console.WriteLine(ex.Message); }
            return "";
        }
    }
}
