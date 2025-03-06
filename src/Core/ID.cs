namespace ChatService.Core;

public readonly struct ID : IEquatable<ID>
{
    public Ulid Value { get; }

    public static ID Generate => new(Ulid.NewUlid());

    public ID(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        if (!Ulid.TryParse(id, out var ulid))
            throw new ArgumentException($"{id} is not a valid {nameof(id)}");

        Value = ulid;
    }

    public ID(Ulid value)
    {
        if (value == Ulid.Empty)
            throw new ArgumentException($"{nameof(value)} is required");

        Value = value;
    }

    public static implicit operator ID(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        if (!Ulid.TryParse(id, out var ulid))
            throw new ArgumentException($"{id} is not a valid {nameof(id)}");

        return new ID(ulid);
    }

    public static implicit operator ID?(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;

        if (!Ulid.TryParse(id, out var ulid))
            throw new ArgumentException($"{id} is not a valid {nameof(id)}");

        return new ID(ulid);
    }

    public static implicit operator ID(Ulid ulid)
    {
        if (ulid == Ulid.Empty)
            throw new ArgumentException($"{nameof(ulid)} cannot be empty");

        return new ID(ulid);
    }

    public static implicit operator ID?(Ulid ulid)
    {
        if (ulid == Ulid.Empty)
            return null;

        return new ID(ulid);
    }

    public override bool Equals(object? obj) => obj is ID obj2 && Equals(obj2);

    public bool Equals(ID other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ID left, ID right) => left.Equals(right);
    public static bool operator !=(ID left, ID right) => !left.Equals(right);

    public override string ToString() => Value.ToString();
}
