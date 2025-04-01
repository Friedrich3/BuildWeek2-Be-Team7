using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.DTOs.Hospitalization;
using BuildWeek2_Be_Team7.DTOs.Owner;
using BuildWeek2_Be_Team7.DTOs.Pet;
using BuildWeek2_Be_Team7.Models.Animali;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2_Be_Team7.Services
{
    public class HospitalizationServices
    {
        private readonly ApplicationDbContext _context;
        public HospitalizationServices(ApplicationDbContext context)
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

        public async Task<bool> AddNewHospit(AddHospitalizationDto addHospitalizationDto)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.PetId == addHospitalizationDto.PetId);
            if (pet == null) { return false; }
            var newHospit = new Hospitalization()
            {
                HospitalizationId = Guid.NewGuid(),
                StartDate = addHospitalizationDto.StartDate,
                PetId = addHospitalizationDto.PetId,
            };
            _context.Hospitalizations.Add(newHospit);
            return await SaveAsync();
        }

        public async Task<EditHospitalizationDto?> EditGetHospit(Guid hospitId)
        {
            var hospit = await _context.Hospitalizations.FirstOrDefaultAsync(p => p.HospitalizationId == hospitId);
            if (hospit == null) { return null; }
            var edithospit = new EditHospitalizationDto()
            {
                StartDate = hospit.StartDate,
                EndDate = hospit.EndDate,
            };
            return edithospit;
        }

        public async Task<bool> EditSaveHospit(Guid hospitId, EditHospitalizationDto editHospitalizationDto)
        {
            var hospit = _context.Hospitalizations.FirstOrDefault(p => p.HospitalizationId == hospitId);
            if (hospit == null) { return false; }
            hospit.StartDate = editHospitalizationDto.StartDate;
            hospit.EndDate = editHospitalizationDto.EndDate;
            return await SaveAsync();
        }

        public async Task<bool> EndRecovery(Guid hospitId)
        {
            var hospit = _context.Hospitalizations.FirstOrDefault(p => p.HospitalizationId == hospitId);
            if (hospit == null) { return false; }
            hospit.EndDate = DateOnly.FromDateTime(DateTime.Now);
            return await SaveAsync();
        }


        public async Task<List<SingleHospitalizationDto>> GetAllHospitActive()
        {
            var ListHospit = new List<SingleHospitalizationDto>();
            var data = await _context.Hospitalizations.Include(p => p.Pet).ThenInclude(p => p.Race).Where(p => p.EndDate == null).ToListAsync();
            if (data.Count == 0) { return ListHospit; }
            ListHospit = data.Select(item => new SingleHospitalizationDto()
            {
                HospitalizationId = item.HospitalizationId,
                StartDate = item.StartDate,
                Pet = new PetHospitalizationDto()
                {
                    PetId = item.Pet.PetId,
                    RegistrationDate = item.Pet.RegistrationDate,
                    Name = item.Pet.Name,
                    Color = item.Pet.Color,
                    Race = item.Pet.Race.Name,
                    Microchip = item.Pet.Microchip,
                }
            }).ToList();
            return ListHospit;
        }

        public async Task<(bool, SinglePetDto?)> GetHospitalizedPetAsync(string microchip)
        {
            try
            {
                var hospitalization = await _context.Hospitalizations.Include(h => h.Pet).ThenInclude(p => p.Race).FirstOrDefaultAsync(h => h.Pet.Microchip == microchip);

                if (hospitalization == null)
                {
                    return (false, null);
                }

                var pet = new SinglePetDto()
                {
                    RegistrationDate = hospitalization.Pet.RegistrationDate,
                    Name = hospitalization.Pet.Name,
                    Color = hospitalization.Pet.Color,
                    Race = hospitalization.Pet.Race.Name,
                    BirthDate = hospitalization.Pet.BirthDate,
                    Microchip = hospitalization.Pet.Microchip
                };

                return (true, pet);
            }
            catch
            {
                return (false, null);
            }
        }
    }
}
