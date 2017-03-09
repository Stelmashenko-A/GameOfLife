using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace LifeHost.Business.GameStorage
{
    public class TimeStampIndex : AbstractIndexCreationTask<Part>
    {
        public TimeStampIndex()
        {
            Map = parts => from part in parts
                select new
                {
                    part.TimeStamp
                };
        }
    }

    public class IndexBuilder:IDisposable
    {
        protected IDocumentStore Store { get; }

        public IndexBuilder()
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

        public void Build()
        {
            new TimeStampIndex().Execute(Store);
        }
    }
}