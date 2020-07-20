using HttpTracker.Data.Collections;

namespace HttpTracker.Data
{
    public interface IData
    {
        DataDictionary Data { get; set; }
    }
}