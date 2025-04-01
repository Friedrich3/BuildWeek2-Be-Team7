using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Models.Animali;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BuildWeek2_Be_Team7.Services
{
    public class PetServices
    {
        private readonly ApplicationDbContext _context;

        public PetServices(ApplicationDbContext context)
        {
            _context = context;

        }
        private async Task<bool> SaveAsync()
        {
            try
            {

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }


        public async Task<bool> New(Pet model)
        {
            try
            {
                _context.Pets.Add(model);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Pet>?> GetAll()
        {
            try
            {
                var result = await _context.Pets
                    .Include(p => p.Race)
                    .Include(p => p.Owner)
                    .Include(p => p.MedicalExams)
                    .Include(p => p.Hospitalizations)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        public async Task<Pet?> GetOne(Guid id)
        {
            try
            {
                var result = await _context.Pets
                    .Include(p => p.Race)
                    .Include(p => p.Owner)
                    .Include(p => p.MedicalExams)
                    .ThenInclude(me => me.Vet)
                    .Include(p => p.Hospitalizations)
                    .FirstOrDefaultAsync(p => p.PetId == id);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch { return null; }
        }

        public async Task<Pet?> GetMicrochip(string microchip)
        {
            try
            {
                var result = await _context.Pets
                .Include(p => p.Race)
                .FirstOrDefaultAsync(p => p.Microchip == microchip);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch { return null; }
        }

        public async Task<bool> Update(Guid id, Pet model)
        {
            var result = await _context.Pets.FirstOrDefaultAsync(p => p.PetId == id);
            if (result == null)
            {
                return false;
            }

            result.Name = model.Name;
            result.Color = model.Color;
            result.RaceId = model.RaceId;
            result.BirthDate = model.BirthDate;
            result.Microchip = model.Microchip;
            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _context.Pets.FindAsync(id);
            if (result == null)
            {
                return false;
            }
            _context.Pets.Remove(result);
            return await SaveAsync();
        }

    }
}
