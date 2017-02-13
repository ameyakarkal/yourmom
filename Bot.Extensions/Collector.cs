using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Bot.Extensions
{
    public static class Collector
    {
        private static readonly CloudBlobClient BlobClient;

        static Collector()
        {
            BlobClient = CloudStorageAccount
                .Parse(ConfigurationManager.AppSettings["Storage.ConnectionString"])
                .CreateCloudBlobClient();
        }

        public static async void Collect(SlackCommand cmd)
        {            
            var blob = BlobClient
                .GetContainerReference("bot")
                .GetBlockBlobReference(DateTime.UtcNow.Ticks.ToString());

            await blob.UploadTextAsync(JsonConvert.SerializeObject(cmd));
        }        
    }
}
