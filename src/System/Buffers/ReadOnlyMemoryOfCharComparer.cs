namespace System.Buffers;

/// <summary>
/// Represents an equality comparer of <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> object,
/// but it can also be created by comparing <see cref="string"/> values as an efficient way in variant collection types
/// like <see cref="HashSet{T}.AlternateLookup{TAlternate}"/>.
/// </summary>
/// <seealso cref="HashSet{T}.AlternateLookup{TAlternate}"/>
public sealed class ReadOnlyMemoryOfCharComparer :
	IAlternateEqualityComparer<ReadOnlySpan<char>, ReadOnlyMemory<char>>,
	IEqualityComparer<ReadOnlyMemory<char>>
{
	/// <summary>
	/// Indicates the singleton object of this type.
	/// </summary>
	public static readonly ReadOnlyMemoryOfCharComparer Instance = new();


	/// <summary>
	/// Initializes a <see cref="ReadOnlyMemoryOfCharComparer"/> instance.
	/// </summary>
	private ReadOnlyMemoryOfCharComparer()
	{
	}


	/// <inheritdoc/>
	public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y) => x.Span.SequenceEqual(y.Span);

	/// <inheritdoc/>
	public bool Equals(ReadOnlySpan<char> alternate, ReadOnlyMemory<char> other) => alternate.SequenceEqual(other.Span);

	/// <inheritdoc/>
	public int GetHashCode(ReadOnlyMemory<char> obj) => GetHashCode(obj.Span);

	/// <inheritdoc/>
	public int GetHashCode(ReadOnlySpan<char> alternate)
	{
		var hc = default(HashCode);
		hc.AddBytes(MemoryMarshal.Cast<char, byte>(alternate));
		return hc.ToHashCode();
	}

	/// <inheritdoc/>
	public ReadOnlyMemory<char> Create(ReadOnlySpan<char> alternate) => alternate.ToArray();
}
