using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Models.Animali;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BuildWeek2_Be_Team7.DTOs.Owner;

namespace BuildWeek2_Be_Team7.Services
{
    public class ClientServices
    {
        private readonly ApplicationDbContext _context;

        public ClientServices(ApplicationDbContext context)
        {
            _context = context;

        }
        private async Task<bool> SaveAsync()
        {
            try
            {

                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> New(Client model)
        {
            try
            {
                _context.Clients.Add(model);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }

        public async Task<Client?> GetOne(string CF)
        {
            try
            {
                var result = await _context.Clients
                    .FirstOrDefaultAsync(p => p.CodiceFiscale == CF);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message); 
                return null; 
            }
        }

        public async Task<GetOwnerResponseDto?> GetOneOwnerCFAsync(string CF)
        {
            try
            {
                var existingOwner = await _context.Clients.Include(c => c.Orders).ThenInclude(o => o.OrderProds).ThenInclude(o => o.Product).Include(c => c.Pets).ThenInclude(p => p.Race).FirstOrDefaultAsync(c => c.CodiceFiscale == CF);

                if (existingOwner == null) 
                {
                    return null;
                }

                var owner = new GetOwnerResponseDto()
                {
                    Name = existingOwner.Name,
                    Surname = existingOwner.Surname,
                    Birthdate = existingOwner.Birthdate,
                    CodiceFiscale = existingOwner.CodiceFiscale,
                    Email = existingOwner.Email,
                    Pets = existingOwner.Pets.Select(p => new SinglePetDto()
                    {
                        RegistrationDate = p.RegistrationDate,
                        Name = p.Name,
                        Color = p.Color,
                        Race = p.Race.Name,
                        BirthDate = p.BirthDate,
                        Microchip = p.Microchip
                    }).ToList(),
                    Orders = existingOwner.Orders != null ? existingOwner.Orders.Select(o => new SingleOrderDto()
                    {
                        Date = o.Date,
                        Products = o.OrderProds.Select(o => new SingleProductDto()
                        {
                            Name = o.Product.Name,
                            Image = o.Product.Image,
                            Price = o.Product.Price,
                            isMed = o.Product.isMed
                        }).ToList()
                    }).ToList() : new List<SingleOrderDto>()
                };

                return owner;
            }
            catch (Exception ex) 
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateOwnerAsync(string CF, string email)
        {
            try
            {
                var owner = await _context.Clients.FirstOrDefaultAsync(c => c.CodiceFiscale == CF);

                if (owner == null) 
                {
                    return false;
                }

                owner.Email = email;

                return await SaveAsync();
            }
            catch (Exception ex) 
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteOwnerAsync(string CF)
        {
            try
            {
                var owner = await _context.Clients.FirstOrDefaultAsync(c => c.CodiceFiscale == CF);

                if (owner == null)
                {
                    return false;
                }

                var pets = await _context.Pets.Where(p => p.OwnerId ==  owner.ClientId).ToListAsync();

                if (pets.Any()) 
                {
                    foreach (var pet in pets)
                    {
                        pet.OwnerId = null;
                    }
                }

                _context.Clients.Remove(owner);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }
    }
}
