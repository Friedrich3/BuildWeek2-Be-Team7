using BuildWeek2_Be_Team7.DTOs.Client;
using BuildWeek2_Be_Team7.DTOs.Pet;
using BuildWeek2_Be_Team7.DTOs.Pharmacy;
using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using System.Security.Claims;

namespace BuildWeek2_Be_Team7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly PharmacyServices _pharmacyServices;
        private readonly ClientServices _clientServices;
        public PharmacyController(PharmacyServices service, ClientServices service2)
        {
            _pharmacyServices = service;
            _clientServices = service2;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _pharmacyServices.GetAll();
                if (result == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong!"
                    });
                }
                var count = result.Count();

                var text = count == 1 ? $"{count} Order found" : $"{count} orders found";

                var order = result.Select(s => new OrderDto
                {
                    Pharmacist = $"Dott.{s.Pharmacist.FirstName} {s.Pharmacist.LastName}",
                    Client = new ClientDto
                    {
                        Name = s.Client.Name,
                        Surname = s.Client.Surname,
                        CodiceFiscale = s.Client.CodiceFiscale,
                        Email = s.Client.Email,
                    },
                    Date = s.Date,
                    Prescription = s.Prescription != null ? new PrescriptionDto
                    {
                        DoctorCf = s.Prescription.DoctorCode,
                        Description = s.Prescription.Description,
                        Date = s.Prescription.Date
                    } : null,
                    Products = s.OrderProds.Select(s => new ProductOrderDto
                    {
                        Name = s.Product.Name,
                        Image = s.Product.Image,
                        Price = s.Product.Price,
                        Quantity = s.Quantity
                    }).ToList(),
                    Total = s.Total,
                }).ToList();
                return Ok(new
                {
                    message = text,
                    data = order
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                var result = await _pharmacyServices.GetOne(id);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var order = new OrderDto
                {
                    Pharmacist = $"Dott.{result.Pharmacist.FirstName} {result.Pharmacist.LastName}",
                    Client = new ClientDto
                    {
                        Name = result.Client.Name,
                        Surname = result.Client.Surname,
                        CodiceFiscale = result.Client.CodiceFiscale,
                        Email = result.Client.Email,
                    },
                    Date = result.Date,
                    Prescription = result.Prescription != null ? new PrescriptionDto
                    {
                        DoctorCf = result.Prescription.DoctorCode,
                        Description = result.Prescription.Description,
                        Date = result.Prescription.Date
                    } : null,
                    Products = result.OrderProds.Select(s => new ProductOrderDto
                    {
                        Name = s.Product.Name,
                        Image = s.Product.Image,
                        Price = s.Product.Price,
                        Quantity = s.Quantity
                    }).ToList(),
                    Total = result.Total,
                };
                return Ok(new
                {
                    data = order
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> New([FromBody] AddOrder model)
        {
            try
            {
                Client Client = null;
                if (model.CodiceFiscaleClient != null)
                {
                    Client = await _clientServices.GetOne(model.CodiceFiscaleClient);
                    if (Client == null)
                    {
                        Client = new Client
                        {
                            ClientId = Guid.NewGuid(),
                            Name = model.NameClient,
                            Surname = model.SurnameClient,
                            Birthdate = model.DateBirthClient,
                            CodiceFiscale = model.CodiceFiscaleClient,
                            Email = model.EmailClient,
                        };
                        await _clientServices.New(Client);
                    }
                }
                var user = User.FindFirst(ClaimTypes.Email)?.Value;

                var userId = await _pharmacyServices.GetUserId(user);

                Prescription ricetta = null;
                if (model.DoctorCf != null)
                {
                    ricetta = new Prescription
                    {
                        Id = Guid.NewGuid(),
                        Description = model.DescriptionPrescription!,
                        DoctorCode = model.DoctorCf,
                        Date = (DateTime)model.DatePrescription!,
                    };
                    await _pharmacyServices.newPrescription(ricetta);
                }

                var newOrder = new Order
                {
                    IdPharmacist = userId,
                    ClientId = Client.ClientId,
                    Date = DateTime.UtcNow,
                    IdPrescription = ricetta.Id,
                    Total = model.Total,
                    OrderProds = model.Products.Select(s => new OrderProd
                    {
                        ProductId = s.IdProduct,
                        Quantity = s.Quantity,
                    }).ToList()
                };

                var result = await _pharmacyServices.New(newOrder);

                return result ? Ok(new { Message = "Order added!" }) : BadRequest(new { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _pharmacyServices.Delete(id);
            return result ? Ok(new { message = "Order Deleted!" }) : BadRequest(new { message = "Something went wrong" });
        }


    }
}
