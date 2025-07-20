using QansNoSqlDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansNoSqlDAL.Abstraction
{
    public interface ITopicRepo
    {
        /// <summary>
        /// validates if the provided topic exists in the topic table
        /// </summary>
        /// <param name="partKey">partition key</param>
        /// <param name="rowKey">row key</param>
        /// <returns>returns a boolean flag that indicates if the topic is already exist</returns>
        Task<bool> ValidateTopicExistasyn(Topic topic);


        /// <summary>
        /// validates if the provided chapter exists in the topic table
        /// </summary>
        /// <param name="partKey">partition key</param>
        /// <param name="rowKey">row key</param>
        /// <returns>returns a boolean flag that indicates if the chapter is already exist</returns>
        Task<bool> validateChapterExistasyn(string TopicKey, string ChapterKey);

        /// <summary>
        /// Add a new a topic to the topic table
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns>returns a boolean flag that indicates if the Topic added sucessfully or not </returns>
        Task<bool> AddTopic(Topic topic);

        /// <summary>
        /// Get all the topic from azure table storage.
        /// </summary>
        /// <returns>List of topic</returns>
        Task<List<Topic>> GetAllTopicsAsync(string partitionkey);
    }
}
