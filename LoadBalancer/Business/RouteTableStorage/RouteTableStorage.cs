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

                var table= session.Query<RouteTable>().FirstOrDefault() ??
                       new RouteTable {Routes = new List<Route>()};
                var hosts = table.Routes.Select(x => x.Host).OrderBy(x=>x).Distinct();
                foreach (var host in hosts)
                {
                    if (table.Routes.Count(x => x.Host == host) > 1)
                    {
                        var lastConnection = table.Routes.Where(x => x.Host == host).Select(x => x.LastConnection).Max();
                        var forRemoving = table.Routes.Where(x => x.Host == host && x.LastConnection != lastConnection).ToList();
                        foreach (var route in forRemoving)
                        {
                            table.Routes.Remove(route);
                        
                        }
                    }
                }

                hosts = table.RequestForAddings.Select(x => x.Host).OrderBy(x => x).Distinct();
                foreach (var host in hosts)
                {
                    if (table.RequestForAddings.Count(x => x.Host == host) > 1)
                    {
                        var lastConnection = table.RequestForAddings.Where(x => x.Host == host).Select(x => x.TimeOfGet).Max();
                        var forRemoving = table.RequestForAddings.Where(x => x.Host == host && x.TimeOfGet != lastConnection).ToList();
                        foreach (var route in forRemoving)
                        {
                            table.RequestForAddings.Remove(route);

                        }
                    }
                }

                session.Store(table);
                session.SaveChanges();
                return table;

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