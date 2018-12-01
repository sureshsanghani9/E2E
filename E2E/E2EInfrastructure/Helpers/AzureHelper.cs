using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace E2EInfrastructure.Helpers
{
    public static class AzureHelper
    {
        public static CloudStorageAccount InitializeAccount()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("BlobStorageConnectionString"));
            return storageAccount;
        }

        public static CloudBlobContainer GetContainer(string containerName)
        {
            CloudBlobClient blobClient = InitializeAccount().CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();

            return container;
        }


        public static void UploadFile(string containerName, string fileName, HttpPostedFileBase file)
        {
            var container = GetContainer(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(!string.IsNullOrEmpty(fileName) ? fileName : file.FileName);
            blockBlob.Properties.ContentType = file.ContentType;
            Stream inputFileStream = file.InputStream;
            using (var fileStream = inputFileStream)
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

    }

    
}
