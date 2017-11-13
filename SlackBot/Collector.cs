using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using SlackBot;

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

        public static async Task Collect(SlackCommand cmd)
        {            
            var blob = BlobClient
                .GetContainerReference("bot")
                .GetBlockBlobReference(DateTime.UtcNow.Ticks.ToString());

            await blob.UploadTextAsync(JsonConvert.SerializeObject(cmd));
        }        
    }
}
