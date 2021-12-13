using System;

namespace AdventOfCode.Year2021.Day13;

public static class StringExtensions
{
	public static (string, string) SplitIntoTwo(this string str, char separator, StringSplitOptions options = StringSplitOptions.None)
	{
		string[] parts = str.Split(separator, options);
		if (parts.Length != 2)
		{
			throw new InvalidOperationException("Cannot split string into two parts");
		}
		return (parts[0], parts[1]);
	}
}
