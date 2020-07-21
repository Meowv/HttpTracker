namespace HttpTracker.Repositories
{
    public interface IHttpTrackerLogRepositoryFactory
    {
        /// <summary>
        /// 创建以年月分表的实例
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        IHttpTrackerLogRepository CreateInstance(string yearMonth);
    }
}