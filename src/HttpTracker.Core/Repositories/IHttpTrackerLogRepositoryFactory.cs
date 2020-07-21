namespace HttpTracker.Repositories
{
    public interface IHttpTrackerLogRepositoryFactory
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IHttpTrackerLogRepository CreateInstance(string name);
    }
}