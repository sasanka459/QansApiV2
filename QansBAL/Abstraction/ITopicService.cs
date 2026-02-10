using QansBAL.DTO;


namespace QansBAL.Abstraction
{
    public interface ITopicService
    {
        /// <summary>
        /// Add topic/chapter to azure no sql db
        /// </summary>
        /// <param name="topic">topic/</param>
        /// <returns>returns true or false indication the save result</returns>
        Task<bool> Add(Topic topic);

        /// <summary>
        /// Get all the topics
        /// </summary>
        /// <returns>List of TopicSummaryDto</returns>
        Task<List<TopicSummaryDto>> GetAllTopic();


        /// <summary>
        /// Get all the chapters
        /// </summary>
        /// <returns>List of TopicSummaryDto</returns>
        Task<List<TopicSummaryDto>> GetChaptersByTopic(string topicKey);

        /// <summary>
        /// validate topic/chapter entity
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns></returns>
        Task<(bool,string)> validateTopic(Topic topic);


        /// <summary>
        /// validate module entity
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns></returns>
        Task<(bool, string)> ValidateModule(Module topic);


        /// <summary>
        /// validate module to the storage
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns></returns>
        Task<bool> AddModule(Module module);
    }
}
