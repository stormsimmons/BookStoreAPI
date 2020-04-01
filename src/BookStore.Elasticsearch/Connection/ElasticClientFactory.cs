using BookStore.Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Elasticsearch.Connection
{
    public static class ElasticClientFactory
    {
        public static IElasticClient CreateElasticClient(IEnumerable<string> hosts, string userName = null, string password = null)
        {
            var connectionSettings = new ConnectionSettings(hosts.Select(x => new Uri(x)).FirstOrDefault())
                .DefaultMappingFor<Book>(x => x.IndexName("books"));

            if (userName != null && password != null)
            {
                connectionSettings.BasicAuthentication(userName, password);
            }

            return new ElasticClient(connectionSettings);
        }
    }
}
