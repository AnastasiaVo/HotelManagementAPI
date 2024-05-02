using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Microsoft.EntityFrameworkCore;


namespace FPH.DataBase.Repositories
{
    public class AccomodationTypeRepository : IAccomodationTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public AccomodationTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNumberOfBedsInAccomodationTypeAsync(int accomodationTypeId)
        {
            var accomodationType = await _context.AccomodationTypes.FindAsync(accomodationTypeId);
            return accomodationType?.NumberOfBeds ?? 0;
        }

        public async Task<IEnumerable<AccomodationTypeEntity>> GetAllAccomodationTypesAsync()
        {
            return await _context.AccomodationTypes.ToListAsync();
        }

        public async Task<AccomodationTypeEntity> GetAccomodationTypeByIdAsync(int id)
        {
            return await _context.AccomodationTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAccomodationTypeAsync(AccomodationTypeEntity accomodationType)
        {
            _context.AccomodationTypes.Add(accomodationType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccomodationTypeAsync(AccomodationTypeEntity accomodationType)
        {
            _context.Entry(accomodationType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccomodationTypeAsync(int id)
        {
            var accomodationType = await _context.AccomodationTypes.FindAsync(id);
            if (accomodationType != null)
            {
                _context.AccomodationTypes.Remove(accomodationType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
