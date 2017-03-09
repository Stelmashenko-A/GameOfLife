using System;
using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

namespace LifeHost.Business.GameStorage
{
    public class GameStorage : IGameStorage
    {
        protected IDocumentStore Store { get; }

        public GameStorage()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080/", // server URL
                DefaultDatabase = "DistributedSystem" // default database
            };
            Store.Initialize();

        }

        public void Save(Part part)
        {
            using (var session = Store.OpenSession())
            {
                session.Store(part);
                session.SaveChanges();
            }
        }

        public Part Get(Guid taskId, int partId)
        {
            using (var session = Store.OpenSession())
            {
                return session.Query<Part>().FirstOrDefault(x => x.TaskId == taskId && x.PartNumber == partId);
            }
        }

        public void RemoveAllParts(Guid taskId)
        {
            using (var session = Store.OpenSession())
            {
                var forDel = session.Query<Part>().Where(x => x.TaskId == taskId);
                foreach (var part in forDel)
                {
                    session.Delete(part);

                }
                session.SaveChanges();
            }
        }

        public void Dispose()
        {
            if (!Store.WasDisposed)
            {
                Store.Dispose();
            }
        }
    }
}