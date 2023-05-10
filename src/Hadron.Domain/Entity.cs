using Hadron.Identifier;

namespace Hadron.Domain;

public class Entity;

public class Entity<TId> : Entity {
    public virtual TId Id { get; init; }

    public Entity(TId id) {
        Id = id;
    }
}