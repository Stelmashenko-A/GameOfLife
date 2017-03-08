using System;
using Raven.Client;
using Raven.Client.Document;

namespace LifeHost.Business.GameStorage
{
    public class GameCleaner : IGameCleaner
    {
        protected IDocumentStore Store { get; }

        public GameCleaner()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080/", // server URL
                DefaultDatabase = "DistributedSystem" // default database
            };
            Store.Initialize();

        }

        public void Dispose()
        {
            Store.Dispose();
        }

        public void Clean()
        {
            using (var session = Store.OpenSession())
            {
                session.Advanced.DeleteByIndex<Part>("TimeStampIndex", x => x.TimeStamp < DateTime.Now.AddHours(-1));
                session.SaveChanges();
            }
        }
    }
}