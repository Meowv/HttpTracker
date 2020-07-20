using Nest;

namespace HttpTracker
{
    public interface IElasticsearchProvider
    {
        IElasticClient GetClient();
    }
}