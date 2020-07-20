using Nest;

namespace HttpTracker
{
    public abstract class ElasticsearchRepositoryBase
    {
        private readonly IElasticsearchProvider _elasticsearchProvider;

        public ElasticsearchRepositoryBase(IElasticsearchProvider elasticsearchProvider)
        {
            _elasticsearchProvider = elasticsearchProvider;
        }

        protected abstract string IndexName { get; }

        protected IElasticClient GetElasticClient => _elasticsearchProvider.GetClient();
    }
}