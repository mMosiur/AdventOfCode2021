using System;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Year2021.Day03;

public static class IntExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetBit(this int value, int index)
	{
		return (value >> index) & 1;
	}
}
