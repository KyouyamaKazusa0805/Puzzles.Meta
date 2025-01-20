namespace System.Numerics;

/// <summary>
/// Represents an enumerator that iterates a <see cref="long"/> or <see cref="ulong"/> value.
/// </summary>
/// <param name="_value">The value to be iterated.</param>
[TypeImpl(
	TypeImplFlags.AllObjectMethods | TypeImplFlags.Disposable,
	OtherModifiersOnDisposableDispose = "readonly",
	ExplicitlyImplsDisposable = true)]
public ref partial struct Int64Enumerator(ulong _value) : IEnumerator<int>
{
	/// <summary>
	/// Indicates the population count of the value.
	/// </summary>
	public readonly int PopulationCount => BitOperations.PopCount(_value);

	/// <summary>
	/// Indicates the bits set.
	/// </summary>
	public readonly ReadOnlySpan<int> Bits => _value.GetAllSets();

	/// <inheritdoc cref="IEnumerator{T}.Current"/>
	public int Current { get; private set; } = -1;

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc cref="BitOperationsExtensions.SetAt(uint, int)"/>
	public readonly int this[int index] => _value.SetAt(index);


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext()
	{
		if (_value == 0)
		{
			return false;
		}

		var mask = (ulong)((long)_value & -(long)_value);
		Current = BitOperations.Log2(mask);
		_value &= ~mask;
		return true;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();
}
