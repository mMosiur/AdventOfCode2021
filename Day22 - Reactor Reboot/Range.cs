namespace AdventOfCode.Year2021.Day22;

public readonly record struct Range(int Start, int End)
{
	public int Length => End - Start + 1;
}
