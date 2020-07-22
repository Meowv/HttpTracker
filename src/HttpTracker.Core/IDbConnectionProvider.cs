using System.Data;

namespace HttpTracker
{
    public interface IDbConnectionProvider
    {
        IDbConnection Connection { get; }
    }
}