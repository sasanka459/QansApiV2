using Microsoft.AspNetCore.Mvc;
using QansBAL.Abstraction;
using QansBAL.DTO;
using System;
using System.Linq; // Added for .Any() LINQ method
using System.Threading.Tasks;

namespace QansAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost("CreateExam")]
        public async Task<IActionResult> CreateExam([FromBody] ExamDTO examDto)
        {
            if (examDto == null)
                return BadRequest(new { message = "Exam data is required" });

            try
            {
                // 1. Fetch all existing exams to check for duplicates
                var existingExams = await _examService.GetAllExams();

                // 2. Check if the code already exists (case-insensitive check is highly recommended)
                bool isDuplicate = existingExams.Any(e =>
                    e.ExamCode.Equals(examDto.ExamCode, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    // Return 409 Conflict so your React frontend knows exactly how to handle it
                    return Conflict(new { message = $"An exam with code '{examDto.ExamCode}' already exists." });
                }
                //
                bool isOrderTaken = existingExams.Any(e => e.SortingOrder == examDto.SortingOrder);

                if (isOrderTaken)
                {
                    // Return a standard 400 Bad Request with a clear message your React frontend can display
                    return BadRequest(new { message = $"Sorting Order {examDto.SortingOrder} is already assigned to another exam. Please select a unique order." });
                }
                // 3. Save if no duplicate is found
                await _examService.SaveExam(examDto);

                return Ok(new
                {
                    message = "Exam created successfully",
                    examCode = examDto.ExamCode
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetAllExams")]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _examService.GetAllExams();
            return Ok(exams);
        }

        [HttpPut("UpdateExam")]
        public async Task<IActionResult> UpdateExam([FromBody] ExamDTO examDto)
        {
            if (examDto == null)
                return BadRequest("Exam data is required.");

            try
            {
                // Optional but recommended: Prevent updating an exam to a code that belongs to ANOTHER exam.
                // Note: You must ensure you aren't checking against the exam you are currently updating.
                // Assuming your DTO has an identifier like 'Id' or 'RowKey'.
                var existingExams = await _examService.GetAllExams();

                // Example check (uncomment and adjust 'e.Id' based on your actual primary key property):
                /*
                bool isDuplicateCode = existingExams.Any(e =>
                    e.ExamCode.Equals(examDto.ExamCode, StringComparison.OrdinalIgnoreCase) &&
                    e.Id != examDto.Id); // Ignore the current exam being updated

                if (isDuplicateCode)
                {
                    return Conflict(new { message = $"The code '{examDto.ExamCode}' is already in use by another exam." });
                }
                */

                await _examService.UpdateExam(examDto);
                return Ok(new { message = "Exam updated successfully", examCode = examDto.ExamCode });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(new { message = ex.Message });

                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}