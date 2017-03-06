using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;

namespace LoadBalancer.Business.RouteTableStorage
{
    public class RouteTableStorage : IDisposable
    {
        protected IDocumentStore Store { get; }
        public RouteTableStorage()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080/", // server URL
                DefaultDatabase = "DistributedSystem" // default database
            };
            Store.Initialize();
            
        }

        public RouteTable Load()
        {
            using (var session = Store.OpenSession())
            {

                return session.Query<RouteTable>().FirstOrDefault() ??
                       new RouteTable {Routes = new List<Route>()};

            }
        }

        public void Save(RouteTable table)
        {
            using (var session = Store.OpenSession())
            {
                session.Store(table, table.Id);
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