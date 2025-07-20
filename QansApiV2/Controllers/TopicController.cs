using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QansBAL.Abstraction;
using QansBAL.DTO;


namespace QansApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService; 
        }

        /// <summary>
        /// Validates and Add Topic and chapter to the storage account azuretable
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns>returns result that indicates if the topic/chapter added sucessfully </returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddTopic([FromBody] Topic topic)
        {
            if (topic == null)
                return BadRequest("Invalid request payload.");

            (bool isValid,string errorMessage) = await _topicService.validateTopic(topic);
            if (isValid)
                return BadRequest(errorMessage);

            await _topicService.Add(topic);
            return Ok("Topic added successfully.");
        }




        /// <summary>
        /// Validates and Add module to the storage account azuretable
        /// </summary>
        /// <param name="topic">topic</param>
        /// <returns>returns result that indicates if the topic/chapter added sucessfully </returns>
        [HttpPost("AddModule")]
        public async Task<IActionResult> AddModule([FromBody] Module module)
        {
            if (module == null)
                return BadRequest("Invalid request payload.");

            (bool isValid, string errorMessage) = await _topicService.ValidateModule(module);
            if (!isValid)
                return BadRequest(errorMessage);

            await _topicService.AddModule(module);
            return Ok("Module added successfully.");
        }


     
        [HttpGet("GetTopic")]
        public async Task<IActionResult> GetTopic()
        {
            var topics= await _topicService.GetAllTopic();

            if (topics == null || !topics.Any()) return NotFound();
            return Ok(topics);
        }

        /// <summary>
        /// Get the chapters or modules based on the partitionkey
        /// </summary>
        /// <param name="topicKey"></param>
        /// <returns></returns>
        [HttpGet("GetChapterOrModule")]
        public async Task<IActionResult> GetChapterByTopic([FromQuery] string topicKey)
        {
            var topics = await _topicService.GetChaptersByTopic(topicKey);

            if (topics == null || !topics.Any()) return NotFound();
            return Ok(topics);
        }
    }
}
