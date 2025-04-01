﻿using BuildWeek2_Be_Team7.DTOs.Hospitalization;
using BuildWeek2_Be_Team7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalizationController : ControllerBase
    {
        private readonly HospitalizationServices _hospitalizationServices;
        public HospitalizationController(HospitalizationServices hospitalizationServices)
        {
            _hospitalizationServices = hospitalizationServices;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin, Veterinario, User")]
        public async Task<IActionResult> AddHospitalization([FromBody] AddHospitalizationDto addHospitalizationDto)
        {
            try
            {
                var result = await _hospitalizationServices.AddNewHospit(addHospitalizationDto);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, something went wrong!" });
                }
                return Ok(new { message = "Hospitalization successfully added" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("edit")]
        [Authorize(Roles = "Admin, Veterinario, User")]
        public async Task<IActionResult> EditHospitalization([FromQuery] Guid hospitId)
        {
            try
            {
                var result = await _hospitalizationServices.EditGetHospit(hospitId);
                if (result == null)
                {
                    return BadRequest(new { message = "Ops, something went wrong!" });
                }
                return Ok(new { message = "Hospitalization successfully recovered" , data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("edit")]
        [Authorize(Roles = "Admin, Veterinario, User")]
        public async Task<IActionResult> EditSaveHospitalization([FromQuery]Guid hospitId, [FromBody] EditHospitalizationDto editHospitalizationDto)
        {
            try
            {
                var result = await _hospitalizationServices.EditSaveHospit(hospitId, editHospitalizationDto);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, something went wrong!" });
                }
                return Ok(new { message = "Hospitalization successfully edited" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("endRecovery")]
        [Authorize(Roles = "Admin, Veterinario, User")]
        public async Task<IActionResult> endRecovery([FromQuery] Guid hospitId)
        {
            try
            {
                var result = await _hospitalizationServices.EndRecovery(hospitId);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, something went wrong!" });
                }
                return Ok(new { message = "Hospitalization ended successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin, Veterinario, User")]
        public async Task<IActionResult> GetAllHospitalization()
        {
            try
            {
                var result = await _hospitalizationServices.GetAllHospitActive();
                if (result == null)
                {
                    return BadRequest(new { message = "Ops, something went wrong!" });
                }
                return Ok(new { message = $"Results found : {result.Count()}", data = result });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
