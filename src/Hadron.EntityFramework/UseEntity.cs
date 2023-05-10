using Hadron.Domain;
using Hadron.Identifier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Reflection;

namespace Hadron.EntityFramework;

public static class UseEntityExtensions {
    public static EntityTypeBuilder<TEntity> UseDomainEntity<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : Entity {
        entity.HasKey("Id");

        return entity;
    }

    public static EntityTypeBuilder<TEntity> UseDomainEntity<TEntity, TId>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : Entity<TId>
        where TId : PrefixedUlid, new() {
        entity.HasKey(nameof(Entity<TId>.Id));
        entity.Property<TId>(nameof(Entity<TId>.Id))
            .HasConversion(id => id.ToString(), id => PrefixedUlid.Parse<TId>(id))
            .HasValueGenerator<IdGenerator<TId>>()
            .HasMaxLength(TotalLength(typeof(TId)));

        return entity;
    }

    static int TotalLength(Type type) {
        var totalFieldInfo = type.GetField("TotalLength", BindingFlags.Public | BindingFlags.Static);
        if (totalFieldInfo is { } info && totalFieldInfo.IsLiteral && totalFieldInfo.IsInitOnly is false) {
            return (int)(info.GetValue(null) ?? 0);
        }

        throw new Exception($"TotalLength has not been found.");
    }

    public class IdGenerator<TId> : ValueGenerator<TId> where TId : PrefixedUlid, new() {
        public override TId Next(EntityEntry entry) {
            if (entry == null) {
                throw new ArgumentNullException(nameof(entry));
            }

            return PrefixedUlid.NewPrefixedUlid<TId>();
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}