using BackOffice.Domain.Primitives;

namespace BackOffice.Domain.Realms;

public sealed class Realm : AggregateRoot
{
    public RealmId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    // TODO: Checkear uso de value objects

    private Realm()
    {

    }

    public Realm(RealmId id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}