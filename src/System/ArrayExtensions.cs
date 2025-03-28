namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Array"/>, especially for one-dimensional array.
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayExtensions
{
	/// <summary>
	/// Initializes an array, using the specified method to initialize each element.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="initializer">The initializer callback method.</param>
	public static void InitializeArray<T>(this T?[] array, ArrayInitializer<T> initializer)
	{
		foreach (ref var element in array.AsSpan())
		{
			initializer(ref element);
		}
	}

	/// <summary>
	/// Flats the specified 2D array into an 1D array.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">An array of elements of type <typeparamref name="T"/>.</param>
	/// <returns>An 1D array.</returns>
	public static T[] Flat<T>(this T[,] @this)
	{
		var result = new T[@this.Length];
		for (var i = 0; i < @this.GetLength(0); i++)
		{
			for (var (j, l2) = (0, @this.GetLength(1)); j < l2; j++)
			{
				result[i * l2 + j] = @this[i, j];
			}
		}
		return result;
	}

	/// <summary>
	/// Converts an array into a <see cref="string"/>.
	/// </summary>
	/// <typeparam name="T">The type of each element inside array.</typeparam>
	/// <param name="this">The array.</param>
	/// <returns>The string representation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[] @this) => @this.ToArrayString(null);

	/// <summary>
	/// Converts an array into a <see cref="string"/>, using the specified formatter method
	/// that can convert an instance of type <typeparamref name="T"/> into a <see cref="string"/> representation.
	/// </summary>
	/// <typeparam name="T">The type of each element inside array.</typeparam>
	/// <param name="this">The array.</param>
	/// <param name="valueConverter">The value converter method.</param>
	/// <returns>The string representation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[] @this, Func<T, string?>? valueConverter)
	{
		valueConverter ??= (static value => value?.ToString());
		return $"[{string.Join(", ", from element in @this select valueConverter(element))}]";
	}

	/// <inheritdoc cref="ToArrayString{T}(T[])"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[OverloadResolutionPriority(1)]
	public static string ToArrayString<T>(this T[][] @this) => @this.ToArrayString(null);

	/// <inheritdoc cref="ToArrayString{T}(T[], Func{T, string?})"/>
	[OverloadResolutionPriority(1)]
	public static string ToArrayString<T>(this T[][] @this, Func<T, string?>? valueConverter)
	{
		var sb = new StringBuilder();
		sb.Append('[').AppendLine();
		for (var i = 0; i < @this.Length; i++)
		{
			var element = @this[i];
			sb.Append("  ").Append(element.ToArrayString(valueConverter));
			if (i != @this.Length - 1)
			{
				sb.Append(',');
			}
			sb.AppendLine();
		}
		sb.Append(']');
		return sb.ToString();
	}

	/// <inheritdoc cref="ToArrayString{T}(T[])"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[,] @this) => @this.ToArrayString(null);

	/// <inheritdoc cref="ToArrayString{T}(T[], Func{T, string?})"/>
	public static string ToArrayString<T>(this T[,] @this, Func<T, string?>? valueConverter)
	{
		valueConverter ??= (static value => value?.ToString());

		var (m, n) = (@this.GetLength(0), @this.GetLength(1));
		var sb = new StringBuilder();
		sb.Append('[').AppendLine();
		for (var i = 0; i < m; i++)
		{
			sb.Append("  ");
			for (var j = 0; j < n; j++)
			{
				var element = @this[i, j];
				sb.Append(valueConverter(element));
				if (j != n - 1)
				{
					sb.Append(", ");
				}
			}
			if (i != m - 1)
			{
				sb.Append(',');
			}
			sb.AppendLine();
		}
		sb.Append(']');
		return sb.ToString();
	}

	/// <summary>
	/// Returns the one-dimensional array representation from the two-dimensional array.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array.</param>
	/// <returns>The <see cref="Span{T}"/> casted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe Span<T> AsSpanUnsafe<T>(this T[,] @this)
	{
		fixed (T* ptr = &@this[0, 0])
		{
			return new(ptr, @this.Length);
		}
	}

	/// <summary>
	/// Returns the one-dimensional array representation from the three-dimensional array.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array.</param>
	/// <returns>The <see cref="Span{T}"/> casted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe Span<T> AsSpanUnsafe<T>(this T[,,] @this)
	{
		fixed (T* ptr = &@this[0, 0, 0])
		{
			return new(ptr, @this.Length);
		}
	}

	/// <summary>
	/// Reinterpret the specified multiple dimensional array into a <see cref="Span{T}"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The multiple dimensional array to be reinterpreted.</param>
	/// <returns>A <see cref="Span{T}"/> instance.</returns>
	public static unsafe Span<T> AsSpanUnsafe<T>(this Array @this) where T : unmanaged
	{
		// Calculate total number of elements.
		// In multiple dimensional array, the result will be the production of lengths from all dimensions.
		var length = @this.Length;

		// Pin the array so that it doesn't move during GC.
		var handle = GCHandle.Alloc(@this, GCHandleType.Pinned);
		try
		{
			// Get a pointer to the beginning of the array.
			return new((void*)handle.AddrOfPinnedObject(), length);
		}
		finally
		{
			// Always free the handle to avoid memory leaks.
			handle.Free();
		}
	}
}
