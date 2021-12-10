namespace AdventOfCode.Year2021.Day10;

public interface ISyntaxResult
{
	public SyntaxResultType Type { get; }
}

public enum SyntaxResultType
{
	Correct,
	Incomplete,
	Corrupted
}
