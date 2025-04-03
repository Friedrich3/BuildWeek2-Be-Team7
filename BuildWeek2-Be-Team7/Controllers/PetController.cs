using BuildWeek2_Be_Team7.DTOs.Client;
using BuildWeek2_Be_Team7.DTOs.Hospitalization;
using BuildWeek2_Be_Team7.DTOs.MedicalExam;
using BuildWeek2_Be_Team7.DTOs.Pet;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using BuildWeek2_Be_Team7.Services;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Veterinario, Admin")]
    public class PetController : ControllerBase
    {
        private readonly PetServices _petServices;
        private readonly ClientServices _clientServices;
        public PetController(PetServices petServices, ClientServices clientServices)
        {
            _petServices = petServices;
            _clientServices = clientServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _petServices.GetAll();
                if (result == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong!"
                    });
                }
                var count = result.Count();

                var text = count == 1 ? $"{count} animal found" : $"{count} animals found";

                var pets = result.Select(s => new PetDto
                {
                    PetId = s.PetId,
                    Name = s.Name,
                    BirthDate = s.BirthDate,
                    Color = s.Color,
                    Race = s.Race.Name,
                    Microchip = s.Microchip,
                    Owner = s.Owner != null ? new OwnerDto
                    {
                        IdOwner = s.Owner.ClientId,
                        Name = s.Owner.Name,
                        Surname = s.Owner.Surname,
                        Birthdate = s.Owner.Birthdate,
                        Email = s.Owner.Email,
                        CodiceFiscale = s.Owner.CodiceFiscale,
                    } : null
                }).ToList();

                return Ok(new
                {
                    message = text,
                    data = pets
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                var result = await _petServices.GetOne(id);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var pet = new SinglePetInfoShow
                {
                    PetId = result.PetId,
                    Name = result.Name,
                    BirthDate = result.BirthDate,
                    Color = result.Color,
                    Race = result.Race.Name,
                    Microchip = result.Microchip,
                    Owner = result.Owner != null ? new OwnerDto
                    {
                        IdOwner = result.Owner.ClientId,
                        Name = result.Owner.Name,
                        Surname = result.Owner.Surname,
                        Birthdate = result.Owner.Birthdate,
                        Email = result.Owner.Email,
                        CodiceFiscale = result.Owner.CodiceFiscale,
                    } : null,
                    RegistrationDate = result.RegistrationDate,
                    PetExams = result.MedicalExams != null ? result.MedicalExams.Select(e => new PetInfoShowExam
                    {
                        ExamDate = e.ExamDate,
                        ExamId = e.ExamId,
                        Diagnosis = e.Diagnosis,
                        State = e.State,
                        VetName = $"Dott. {e.Vet.LastName} {e.Vet.FirstName}",
                        Treatment = e.Treatment
                    }).ToList() : null,
                    PetHospitalization = result.Hospitalizations != null ? result.Hospitalizations.Select(h => new PetInfoShowHospital
                    {
                        HospitalizationId = h.HospitalizationId,
                        StartDate = h.StartDate,
                        EndDate = h.EndDate != null ? (DateOnly)h.EndDate : null
                    }).ToList() : null
                };
                return Ok(new
                {
                    data = pet
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [AllowAnonymous]
        [HttpGet("search/{microchip}")]
        public async Task<IActionResult> GetFromMicrochip(string microchip)
        {
            try
            {
                var result = await _petServices.GetMicrochip(microchip);
                if (result == null)
                {
                    return NoContent();
                }
                var pet = new PetMicrochip
                {
                    Name = result.Name,
                    BirthDate = result.BirthDate,
                    Color = result.Color,
                    Race = result.Race.Name,
                    Microchip = result.Microchip
                };
                return Ok(new
                {
                    data = pet
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> New([FromBody] AddPet pet)
        {
            try
            {
                Client Owner = null;
                if (pet.CodiceFiscale != null)
                {
                    Owner = await _clientServices.GetOne(pet.CodiceFiscale);
                    if (Owner == null)
                    {
                        Owner = new Client
                        {
                            ClientId = Guid.NewGuid(),
                            Name = pet.NameOwner,
                            Surname = pet.Surname,
                            Birthdate = (DateOnly)pet.BirthdateOwner,
                            CodiceFiscale = pet.CodiceFiscale,
                            Email = pet.Email,
                        };
                        await _clientServices.New(Owner);
                    }
                }
                var newPet = new Pet
                {
                    RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Name = pet.Name,
                    BirthDate = pet.BirthDate,
                    Color = pet.Color,
                    Microchip = pet.Microchip ?? null,
                    OwnerId = Owner != null ? Owner.ClientId : null,
                    RaceId = pet.Race,
                };
                var result = await _petServices.New(newPet);
                return result ? Ok(new { Message = "Pet successfully added!" }) : BadRequest(new { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] EditPet model)
        {
            var pet = new Pet
            {
                PetId = id,
                Name = model.Name,
                Color = model.Color,
                BirthDate = model.BirthDate,
                RaceId = model.Race,
                Microchip = model.Microchip ?? null,
            };
            var result = await _petServices.Update(id, pet);
            return result ? Ok(new { message = "Pet successfully updated!" }) : BadRequest(new { message = "Something went wrong" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _petServices.Delete(id);
            return result ? Ok(new { message = "Pet successfully deleted!" }) : BadRequest(new { message = "Something went wrong" });
        }

        [HttpGet("Owner")]
        public async Task<IActionResult> GetOneOwnerCF([FromQuery] string CF)
        {
            try
            {
                var owner = await _clientServices.GetOneOwnerCFAsync(CF);

                return owner != null ? Ok(new { message = "Owner found", Owner = owner }) : BadRequest(new { message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Owner")]
        public async Task<IActionResult> UpdateOwner([FromQuery] string CF, [FromBody] string email)
        {
            try
            {
                var result = await _clientServices.UpdateOwnerAsync(CF, email);

                return result ? Ok(new { message = "Email successfully updated!" }) : BadRequest(new { messagge = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Owner")]
        public async Task<IActionResult> DeleteOwner([FromQuery] string CF)
        {
            try
            {
                var result = await _clientServices.DeleteOwnerAsync(CF);

                return result ? Ok(new { message = "Email successfully deleted!" }) : BadRequest(new { messagge = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
