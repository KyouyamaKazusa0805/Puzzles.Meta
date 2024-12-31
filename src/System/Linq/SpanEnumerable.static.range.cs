namespace System.Linq;

public partial class SpanEnumerable
{
	/// <summary>
	/// Create a range of variables that starts with 0, incrementing values and putting them into the other positions.
	/// </summary>
	/// <param name="count">The number of elements created.</param>
	/// <returns>The result sequence [0, 1, 2, 3, ..].</returns>
	public static ReadOnlySpan<int> Range(int count) => Range(0, count, static previous => previous + 1);

	/// <summary>
	/// Create a range of variables that starts with <paramref name="start"/>,
	/// incrementing values and putting them into the other positions.
	/// </summary>
	/// <param name="start">The start value.</param>
	/// <param name="count">The number of elements created.</param>
	/// <returns>The result sequence.</returns>
	public static ReadOnlySpan<int> Range(int start, int count) => Range(start, count, static previous => previous + 1);

	/// <summary>
	/// Create a range of variables that starts with the specified value, and iterates the value to create followed values.
	/// </summary>
	/// <typeparam name="T">The type of the target value.</typeparam>
	/// <param name="start">The start value.</param>
	/// <param name="count">The number of elements created.</param>
	/// <param name="nextCreator">The creator method that create a value from the previous value.</param>
	/// <returns>The result sequence.</returns>
	public static ReadOnlySpan<T> Range<T>(T start, int count, Func<T, T> nextCreator)
	{
		var result = new T[count];
		result[0] = start;

		for (var i = 1; i < count; i++)
		{
			result[i] = nextCreator(result[i - 1]);
		}
		return result;
	}
}
