using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day10;

public class CorruptedSyntaxResult : ISyntaxResult
{
	public SyntaxResultType Type => SyntaxResultType.Corrupted;
	public char FirstIllegalCharacter { get; }

	public static readonly IReadOnlyDictionary<char, int> ScoreTable = new Dictionary<char, int>
		{
			{ ')', 3 },
			{ ']', 57 },
			{ '}', 1197 },
			{ '>', 25137 }
		};

	public CorruptedSyntaxResult(char firstIllegalCharacter)
	{
		FirstIllegalCharacter = firstIllegalCharacter;
	}

	public int Score => ScoreTable[FirstIllegalCharacter];
}
