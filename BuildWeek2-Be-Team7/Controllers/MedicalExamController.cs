using System.Security.Claims;
using BuildWeek2_Be_Team7.DTOs.MedicalExam;
using BuildWeek2_Be_Team7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Veterinario")]
    public class MedicalExamController : ControllerBase
    {
        private readonly MedicalExamServices _medicalExamServices;

        public MedicalExamController(MedicalExamServices medicalExamServices)
        {
            _medicalExamServices = medicalExamServices;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMedical([FromQuery] string order = null)
        {
            try
            {
                var result = await _medicalExamServices.GetAllExam(order);
                if (result == null)
                {
                    return BadRequest(new { message = "ops, something went wrong!" });
                }
                return Ok(new { message = $"Results found : {result.Count()}", data = result });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExam([FromBody] AddMedicalExam addMedicalExam)
        {
            try
            {
                var result = await _medicalExamServices.AddNewExam(addMedicalExam, ClaimTypes.Email);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, Something went wrong!" });
                }
                return Ok(new { message = "Medical exam added correctly" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("edit")]
        public async Task<IActionResult> GetExam([FromQuery] string id)
        {
            try
            {
                var exam = await _medicalExamServices.GetExamById(id);
                if(exam == null)
                {
                    return BadRequest(new { message = "Ops, Something went wrong!" });
                }
                return Ok(new { message = "Result Found!", data = exam });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("saveEdit")]
        public async Task<IActionResult> SaveExam([FromQuery] string id, [FromBody] MedicalExamRequestDto medicalExamRequestDto)
        {
            try
            {
                var result = await _medicalExamServices.EditExam(id, medicalExamRequestDto, ClaimTypes.Email);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, Something went wrong!" });
                }
                return Ok(new { message = "Medical exam edited correctly" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteExam([FromQuery] string id)
        {
            try
            {
                var result = await _medicalExamServices.DeleteExam(id);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, Something went wrong!" });
                }
                return Ok(new { message = "Medical exam deleted correctly" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





    }
}
