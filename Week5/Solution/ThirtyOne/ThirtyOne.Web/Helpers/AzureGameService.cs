using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThirtyOne.Shared.Models;

namespace ThirtyOne.Web.Helpers
{
    public class AzureGameService : IGameService
    {
        private CloudStorageAccount account;
        private CloudBlobClient client;
        private CloudBlobContainer container;
        public AzureGameService()
        {
            account = CloudStorageAccount.Parse("");
            client=account.CreateCloudBlobClient();
            container = client.GetContainerReference("games");
            container.CreateIfNotExists();
        }

        public void DeleteGame(int id)
        {
            container.GetBlockBlobReference(id.ToString()).Delete();
        }

        public bool GameExist(int id)
        {
            return container.GetBlockBlobReference(id.ToString()).Exists();
        }

        public Game LoadGame(int id)
        {
            var blob = container.GetBlockBlobReference(id.ToString());
            var reader = new StreamReader(blob.OpenRead());
            string json = reader.ReadToEnd();
            reader.Close();
            return Game.DeserializeGame(json);
        }

        public void SaveGame(Game g)
        {
            var blob = container.GetBlockBlobReference(g.GameId.ToString());
            var writer = new StreamWriter(blob.OpenWrite());
            writer.Write(g.SerializeGame());
            writer.Close();
        }
    }
}
