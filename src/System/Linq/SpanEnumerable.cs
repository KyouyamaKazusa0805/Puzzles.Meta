namespace System.Linq;

/// <summary>
/// Provides LINQ-based extension methods on <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <seealso cref="ReadOnlySpan{T}"/>
public static partial class SpanEnumerable;

/// <summary>
/// This type provides a way to record LINQ methods and interfaces implemented on <see cref="ReadOnlySpan{T}"/>.
/// This recording will be consumed by future C# <see langword="extension"/> feature
/// that may be allowed implementing interface types with some types that I cannot modify them,
/// just like <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <typeparam name="T">The generic type argument.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
file abstract class __ImplementedTypes<T> : IEnumerable<T>,
	IAggregateMethod<__ImplementedTypes<T>, T>,
	IAnyAllMethod<__ImplementedTypes<T>, T>,
	IAverageMethod<__ImplementedTypes<T>, T>,
	ICountMethod<__ImplementedTypes<T>, T>,
	ICastMethod<__ImplementedTypes<T>, T>,
	IFirstLastMethod<__ImplementedTypes<T>, T>,
	IGroupByMethod<__ImplementedTypes<T>, T>,
	IIndexMethod<__ImplementedTypes<T>, T>,
	IJoinMethod<__ImplementedTypes<T>, T>,
	ILeftJoinMethod<__ImplementedTypes<T>, T>,
	IMinMaxMethod<__ImplementedTypes<T>, T>,
	IOfTypeMethod<__ImplementedTypes<T>, T>,
	IOrderByMethod<__ImplementedTypes<T>, T>,
	IRangeMethod<__ImplementedTypes<T>, T>,
	IRightJoinMethod<__ImplementedTypes<T>, T>,
	ISelectManyMethod<__ImplementedTypes<T>, T>,
	ISelectMethod<__ImplementedTypes<T>, T>,
	ISkipMethod<__ImplementedTypes<T>, T>,
	ISumMethod<__ImplementedTypes<T>, T>,
	ITakeMethod<__ImplementedTypes<T>, T>,
	IWhereMethod<__ImplementedTypes<T>, T>
	where T :
		IComparable<T>,
		IComparisonOperators<T, T, bool>,
		IMinMaxValue<T>,
		INumberBase<T>
{
	IEnumerator IEnumerable.GetEnumerator() => throw null!;

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw null!;

	static __ImplementedTypes<T> IRangeMethod<__ImplementedTypes<T>, T>.Range(int start, int count) => throw null!;
}
