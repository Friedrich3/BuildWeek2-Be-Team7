using BuildWeek2_Be_Team7.Data;

namespace BuildWeek2_Be_Team7.Services
{
    public class PetServices
    {
        private readonly ApplicationDbContext _context;

        public PetServices(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync() > 0;
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
