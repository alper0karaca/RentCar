namespace RentCarServer.Domain.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = new IdentityId(Guid.CreateVersion7()); // yeni sıralanabilir GUİD versiyonu
        IsActive = true;
    }
    // Audit properties
    public IdentityId Id { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; } // offsett +3 için
    public IdentityId CreatedBy { get; private set; } = default!;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IdentityId? UpdatedBy { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public IdentityId? DeletedBy { get; private set; }

    public void SetStatus(bool isActive)
    {
        IsActive = isActive;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}

// DDD Value objects 
public sealed record IdentityId(Guid Value)
{
    public static implicit operator Guid(IdentityId id) => id.Value;
    public static implicit operator string(IdentityId id) => id.Value.ToString();
};