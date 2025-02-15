namespace System.Text.RegularExpressions;

/// <summary>
/// Provides with extension methdos on <see cref="ValueMatch"/>.
/// </summary>
/// <seealso cref="ValueMatch"/>
public static class ValueMatchExtensions
{
	/// <summary>
	/// Try to get the target match string at the specified position the current instance specified.
	/// </summary>
	/// <param name="this">Indicates the instance.</param>
	/// <param name="originalString">The original string.</param>
	/// <returns>The target string.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<char> MatchString(this ValueMatch @this, ReadOnlySpan<char> originalString)
		=> originalString.Slice(@this.Index, @this.Length);
}
