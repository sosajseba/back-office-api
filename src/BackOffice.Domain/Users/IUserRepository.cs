namespace BackOffice.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task Add(User user);
    Task<List<User>> GetAll();
    Task<bool> Update(User user);
    Task<bool> Delete(UserId id);
}