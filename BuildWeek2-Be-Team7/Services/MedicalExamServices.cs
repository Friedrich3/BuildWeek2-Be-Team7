﻿using System.Security.Claims;
using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.DTOs.MedicalExam;
using BuildWeek2_Be_Team7.Models.Animali;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2_Be_Team7.Services
{
    public class MedicalExamServices
    {
        private readonly ApplicationDbContext _context;
        public MedicalExamServices(ApplicationDbContext context)
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

        public async Task<bool> AddNewExam(AddMedicalExam addMedicalExam , string userEmail)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null) { return false; }
            var newExam = new MedicalExam()
            {
                ExamId = Guid.NewGuid(),
                ExamDate = addMedicalExam.ExamDate,
                PetId = Guid.Parse(addMedicalExam.PetId),
                State = "Pending",
                VetId = addMedicalExam.VetId,
                Treatment = addMedicalExam.Treatment,
                Diagnosis = addMedicalExam.Diagnosis,
                LastModified = $"Dott.{user.LastName} {user.FirstName}"
            };
            _context.MedicalExams.Add(newExam);
            return await SaveAsync();
        }

        public async Task<MedicalExamResponseDto?>GetExamById(string id)
        {
            var data = await _context.MedicalExams.Include(p=>p.Vet).FirstOrDefaultAsync(p=> p.ExamId.ToString() == id);
            if (data == null) return null;
            var exam = new MedicalExamResponseDto()
            {
                ExamId = data.ExamId,
                ExamDate = data.ExamDate,
                Treatment = data.Treatment,
                Diagnosis = data.Diagnosis,
                State = data.State,
                Vet = new VeterinarioDto()
                {
                    VetId = data.Vet.Id,
                    LastName = data.Vet.LastName,
                    FirstName = data.Vet.FirstName,
                },
                LastModified = data.LastModified,
            };
            return exam;
        }

        public async Task<bool> EditExam(string id,MedicalExamRequestDto medicalExamRequestDto, string userEmail)
        {
            var medExam = await _context.MedicalExams.FirstOrDefaultAsync(p => p.ExamId.ToString() == id);
            if( medExam == null ) return false;
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null)  return false; 
            medExam.ExamDate = medicalExamRequestDto.ExamDate;
            medExam.Treatment = medicalExamRequestDto.Treatment;
            medExam.Diagnosis = medicalExamRequestDto.Diagnosis;
            medExam.State = medicalExamRequestDto.State;
            medExam.VetId = medicalExamRequestDto.VetId;
            medExam.LastModified = $"Dott. {user.LastName} {user.FirstName}";
            return await SaveAsync();
        }

        public async Task<bool> DeleteExam(string id)
        {
            var medExam = await _context.MedicalExams.FirstOrDefaultAsync(p => p.ExamId.ToString() == id);
            if ( medExam == null ) { return false; }
            _context.Remove(medExam);
            return await SaveAsync();
        }

        public async Task<List<SingleMedicalExamDto>> GetAllExam(string order)
        {
            var ExamList =  new List<SingleMedicalExamDto>();

            List<MedicalExam> data;
            switch (order){
                case "asc":
                    data = await _context.MedicalExams.Include(p => p.Vet).Include(p => p.Pet).ThenInclude(p => p.Owner).OrderBy(p => p.ExamDate).ToListAsync();
                    break;

                case "desc":
                    data = await _context.MedicalExams.Include(p => p.Vet).Include(p => p.Pet).ThenInclude(p => p.Owner).OrderByDescending(p => p.ExamDate).ToListAsync();
                    break;

                default:
                    data = await _context.MedicalExams.Include(p => p.Vet).Include(p => p.Pet).ThenInclude(p => p.Owner).ToListAsync();
                    break;
            }

            if ( data == null ) { return ExamList; }
            ExamList = data.Select(item => new SingleMedicalExamDto()
            {
                ExamId = item.ExamId,
                ExamDate = item.ExamDate,
                State = item.State,
                VetName = $"Dott. {item.Vet.LastName} {item.Vet.FirstName}",
                PetName = item.Pet.Name ,
                OwnerName = item.Pet.Owner != null ? $"{item.Pet.Owner.Name} {item.Pet.Owner.Surname}" : null,
            }).ToList();
            return ExamList;
        }

        public async Task<List<VeterinarioDto>> GetAllVets()
        {
            var Vetlist = new List<VeterinarioDto>();
            var data = await _context.ApplicationUserRoles.Include(p => p.User).Include(p => p.Role).Where(p => p.Role.Name == "Veterinario").ToListAsync();
            if (data == null) { return Vetlist; }
            Vetlist = data.Select(item => new VeterinarioDto() 
            {
                VetId = item.User.Id.ToString(),
                FirstName = item.User.FirstName,
                LastName = item.User.LastName,
            }).ToList();
            return Vetlist;
        }

    }
}
