using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IAccomodationTypeRepository
    {
        Task<IEnumerable<AccomodationTypeEntity>> GetAllAccomodationTypesAsync();
        Task<AccomodationTypeEntity> GetAccomodationTypeByIdAsync(int id);
        Task AddAccomodationTypeAsync(AccomodationTypeEntity accomodationType);
        Task UpdateAccomodationTypeAsync(AccomodationTypeEntity accomodationType);
        Task DeleteAccomodationTypeAsync(int id);
        Task<int> GetNumberOfBedsInAccomodationTypeAsync(int accomodationTypeId);
    }
}
