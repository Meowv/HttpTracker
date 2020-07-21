using HttpTracker.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpTracker
{
    public class MongoDbProvider : IMongoDbProvider
    {
        public MongoDbProvider(IOptions<HttpTrackerMongoDbOptions> options)
        {
            Options = options.Value;

            if (Options.Services == null)
                throw new Exception("MongoDb 配置有误，请检查。");

            var settings = new MongoClientSettings();
            var servers = new List<MongoServerAddress>();

            foreach (var item in Options.Services)
            {
                var host = item.Split(":");
                servers.Add(new MongoServerAddress(host.First(), Convert.ToInt32(host.Last())));
            }

            settings.Servers = servers;

            if (!string.IsNullOrEmpty(Options.Username) && !string.IsNullOrEmpty(Options.Password))
            {
                settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
            }

            if (Options.ConnectionMode.ToLower() == "replicaset")
            {
                settings.ConnectionMode = ConnectionMode.ReplicaSet;
                settings.ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            }

            Client = new MongoClient(settings);

            Database = Client.GetDatabase(Options.DatabaseName);
        }

        public HttpTrackerMongoDbOptions Options { get; }

        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }
    }
}