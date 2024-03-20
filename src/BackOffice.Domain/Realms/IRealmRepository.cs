namespace BackOffice.Domain.Realms;

public interface IRealmRepository
{
    Task<Realm?> GetByIdAsync(RealmId id);
    Task Add(Realm user);
    Task<List<Realm>> GetAll();
    Task<bool> Update(Realm user);
    Task<bool> Delete(RealmId id);
}