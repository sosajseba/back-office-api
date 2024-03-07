using BackOffice.Domain.Primitives;

namespace BackOffice.Domain.Users;

public sealed class User : AggregateRoot
{
    public UserId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; } = string.Empty;
    public bool Active { get; set; }
    // TODO: Checkear uso de value objects

    private User()
    {

    }

    public User(UserId id, string name, string lastName, string email, bool active)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Active = active;
    }
}