using Azure;
using QansBAL.Abstraction;
using QansBAL.DTO;
using QansBAL.Helper;
using QansNoSqlDAL.Abstraction;
using System.Collections.Concurrent;
using entity=QansNoSqlDAL.Entities;

namespace QansBAL.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepo _topicRepoService;

        public TopicService(ITopicRepo topicRepo)
        {
            _topicRepoService = topicRepo;
                
        }
        public async Task<bool> Add(Topic topic)
        {
         return  await  _topicRepoService.AddTopic(MapTopicToEntity(topic));
            
        }

        public async Task<bool> AddModule(Module module)
        {
            return await _topicRepoService.AddTopic(MapModuleToentity(module));
        }
        public async Task<List<TopicSummaryDto>> GetAllTopic()
        {
            var topics= await _topicRepoService.GetAllTopicsAsync("Topic");

            var topicSummaries= topics.Select(x=> new TopicSummaryDto { PartitionKey=x.PartitionKey,RowKey=x.RowKey, Name=x.Name}).ToList();

            return topicSummaries;
        }
        public async Task<List<TopicSummaryDto>> GetChaptersByTopic(string topicKey)
        {
            var topics = await _topicRepoService.GetAllTopicsAsync(topicKey);

            var topicSummaries = topics.Select(x => new TopicSummaryDto { PartitionKey = x.PartitionKey, RowKey = x.RowKey, Name = x.Name }).ToList();

            return topicSummaries;
        }

        public async Task<(bool, string)> ValidateModule(Module module)
        {
            if (string.IsNullOrWhiteSpace(module.ChapterKey))
                return (false, "Module must have a parent Chapter.");

            if (string.IsNullOrWhiteSpace(module.TopicKey))
                return (false, "Module must have a parent Topic.");

            //Check if parent chapter exist
            bool exists = await _topicRepoService.validateChapterExistasyn(module.TopicKey,module.ChapterKey);
            if (!exists)
                return (false, "Parent chapter does not exist.");

            return (true,string.Empty);
        }

        public async Task<(bool, string)> validateTopic(Topic topic)
        {
            if (topic.ContentType == "Topic")
            {
                if (!string.IsNullOrWhiteSpace(topic.ParentTopic))
                    return (false, "Topic cannot have a parent topic.");
            }
            else if (topic.ContentType == "Chapter")
            {
                if (string.IsNullOrWhiteSpace(topic.ParentTopic))
                    return (false, "Chapter must have a parent topic.");

                // Check if parent exists as a Topic
                bool exists = await _topicRepoService.ValidateTopicExistasyn(MapTopicToEntity(topic));
                if (!exists)
                    return (false, "Parent topic does not exist or is not of ContentType 'Topic'.");
            }
            else
            {
                return (false, "Invalid ContentType. Must be 'Topic' or 'Chapter'.");
            }

            return (true, string.Empty);
        }

        private entity.Topic MapTopicToEntity(Topic topic)
        {
            string partitionKey=string.Empty,rowKey=string.Empty;

            //Generate partition key
            if(topic.ContentType =="Topic")
            {
                partitionKey = "Topic";
                rowKey = topic.Name.GetStorageKey();
                return new entity.Topic()
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ContentType = topic.ContentType,
                    Name = topic.Name,
                    Description = topic.Description,
                    ETag=ETag.All
                };
            }
            else
            {
                partitionKey = topic.ParentTopic.GetStorageKey();
                rowKey = topic.Name.GetStorageKey();
                return new entity.Topic()
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ContentType = topic.ContentType,
                    Name = topic.Name,
                    Description = topic.Description,
                    ParentTopic=topic.ParentTopic,
                    ETag = ETag.All
                };
            }

        }


        private entity.Topic MapModuleToentity(Module module)
        {
           string partitionKey = module.ChapterKey;
           string rowKey = module.Name.GetStorageKey();
            return new entity.Topic()
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                ContentType = "Module",
                Name = module.Name,
                Description = module.Description,
                ParentTopic = module.TopicKey,
                ETag = ETag.All
            };

        }

    
    }
}
