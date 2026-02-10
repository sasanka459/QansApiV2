using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using QansBAL.Abstraction;
using QansBAL.DTO;
using QansBAL.Services;

namespace QansApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService=questionService;
        }

        [HttpPost(Name = "SaveQuestion")]
        public async Task<IActionResult> SaveQuestion(Question qus)
        {
            if (qus == null)  return BadRequest("Null question is not accepted.");

            try
            {
                await _questionService.SaveQuestion(qus);
                return Ok("Question saved sucessfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred.",
                    Details = ex.Message 
                });
            }

           
        }
    }
}
