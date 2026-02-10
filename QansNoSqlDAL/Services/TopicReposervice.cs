using Azure;
using Azure.Data.Tables;
using QansNoSqlDAL.Abstraction;
using QansNoSqlDAL.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QansNoSqlDAL.Services
{
    public class TopicReposervice : ITopicRepo
    {
        private readonly TableClient _topicTableClient;
        
        public TopicReposervice(TableServiceClient tableClient )
        {
            _topicTableClient = tableClient.GetTableClient("tblTopic");
        }
        public async Task<bool> AddTopic(Topic topic)
        {
            try
            {
                await _topicTableClient.AddEntityAsync(topic);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Addlogging and return false
                throw ex;
            }
           
        }

        public async Task<List<Topic>> GetAllTopicsAsync(string partitionkey)
        {
            var topics = new List<Topic>();
            await foreach (var topic in _topicTableClient.QueryAsync<Topic>(topic => topic.PartitionKey == partitionkey ))
            {
                topics.Add(topic);
            }
            return topics;
        }



        public async Task<bool> validateChapterExistasyn(string TopicKey, string ChapterKey)
        {
            try
            {
                var response = await _topicTableClient.GetEntityAsync<Topic>(TopicKey, ChapterKey);
                return response != null;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return false;
            }
        }

        public async Task<bool> ValidateTopicExistasyn(Topic topic)
        {
            try
            {
                var response = await _topicTableClient.GetEntityAsync<Topic>(topic.PartitionKey, topic.RowKey);
                return response != null;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return false;
            }
        }



    }
}
