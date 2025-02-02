namespace System.Numerics;

/// <summary>
/// Defines an enumerator type that iterates on bits of an integer of generic type.
/// </summary>
/// <typeparam name="TInteger">The type of the integer that supports for iteration on bits.</typeparam>
/// <param name="_value">The integer to be iterated.</param>
/// <param name="_bitsCount">The integer of bits to be iterated.</param>
[TypeImpl(
	TypeImplFlags.AllObjectMethods | TypeImplFlags.Disposable,
	OtherModifiersOnDisposableDispose = "readonly",
	ExplicitlyImplsDisposable = true)]
public ref partial struct GenericIntegerEnumerator<TInteger>(TInteger _value, int _bitsCount) : IBitEnumerator
#if NUMERIC_GENERIC_TYPE
	where TInteger : IBitwiseOperators<TInteger, TInteger, TInteger>, INumber<TInteger>, IShiftOperators<TInteger, int, TInteger>
#else
	where TInteger :
		IAdditiveIdentity<TInteger, TInteger>,
		IBitwiseOperators<TInteger, TInteger, TInteger>,
		IEqualityOperators<TInteger, TInteger, bool>,
		IMultiplicativeIdentity<TInteger, TInteger>,
		IShiftOperators<TInteger, int, TInteger>
#endif
{
	/// <inheritdoc/>
	public readonly int PopulationCount
#if NUMERIC_GENERIC_TYPE
		=> TInteger.PopCount(_value);
#else
		=> Bits.Length;
#endif

	/// <inheritdoc/>
	public readonly ReadOnlySpan<int> Bits
	{
		get
		{
			var enumerator = new GenericIntegerEnumerator<TInteger>(_value, _bitsCount);
			var result = new List<int>();
			while (enumerator.MoveNext())
			{
				result.Add(enumerator.Current);
			}
			return result.AsSpan();
		}
	}

	/// <inheritdoc cref="IEnumerator{TNumber}.Current"/>
	public int Current { get; private set; } = -1;

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc/>
	public readonly int this[int index] => Bits[index];


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext()
	{
		while (++Current < _bitsCount)
		{
			if (
#if NUMERIC_GENERIC_TYPE
				(_value >> Current & TInteger.One) != TInteger.Zero
#else
				(_value >> Current & TInteger.MultiplicativeIdentity) != TInteger.AdditiveIdentity
#endif
			)
			{
				return true;
			}
		}
		return false;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();
}
