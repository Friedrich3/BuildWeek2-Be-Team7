using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.Models.Animali;
using BuildWeek2_Be_Team7.Models.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BuildWeek2_Be_Team7.Services
{
    public class PharmacyServices
    {
        private readonly ApplicationDbContext _context;
        public PharmacyServices(ApplicationDbContext context)
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

        public async Task<List<Order>?> GetAll()
        {
            try
            {
                var result = await _context.Orders
                    .Include(p => p.Prescription)
                    .Include(p => p.Pharmacist)
                    .Include(p => p.OrderProds)
                    .ThenInclude(p => p.Product)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        public async Task<Order?> GetOne(Guid id)
        {
            try
            {
                var result = await _context.Orders
                    .Include(p => p.Prescription)
                    .Include(p => p.Pharmacist)
                    .Include(p => p.OrderProds)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch { return null; }
        }
        public async Task<bool> New(Order model)
        {
            try
            {
                _context.Orders.Add(model);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }

        public async Task<string> GetUserId(string email)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                return result.Id;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> newPrescription(Prescription model)
        {
            try
            {
                _context.Prescriptions.Add(model);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _context.Orders.FindAsync(id);
            if (result == null)
            {
                return false;
            }
            _context.Orders.Remove(result);
            return await SaveAsync();
        }

    }
}
