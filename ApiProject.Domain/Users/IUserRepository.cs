namespace ApiProject.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    void Update(User user);
    void Remove(User user);

    Task SaveChangesAsync();
}
