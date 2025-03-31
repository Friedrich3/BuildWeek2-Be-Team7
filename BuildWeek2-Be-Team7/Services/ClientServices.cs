using BuildWeek2_Be_Team7.Data;
using BuildWeek2_Be_Team7.Models;
using BuildWeek2_Be_Team7.Models.Animali;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

        public async Task<Client?> GetOne(string email)
        {
            try
            {
                var result = await _context.Clients
                    .FirstOrDefaultAsync(p => p.Email == email);
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

    }
}
