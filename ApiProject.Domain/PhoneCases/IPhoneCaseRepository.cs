namespace ApiProject.Domain.PhoneCases;
public interface IPhoneCaseRepository
{
    Task<PhoneCase?> GetByIdAsync(int id);
    Task<IEnumerable<PhoneCase>> GetRangeAsync(short pageNumber, short pageSize = 10);
    Task AddAsync(PhoneCase phoneCase);
    void Update(PhoneCase phoneCase);
    void Remove(PhoneCase phoneCase);

    Task SaveChangesAsync();
}
