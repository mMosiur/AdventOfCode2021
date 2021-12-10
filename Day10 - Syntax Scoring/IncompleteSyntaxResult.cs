using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day10;

public class IncompleteSyntaxResult : ISyntaxResult
{
	public SyntaxResultType Type => SyntaxResultType.Incomplete;

	public string CompletionString { get; }

	public static readonly IReadOnlyDictionary<char, int> ScoreTable = new Dictionary<char, int>
	{
		{ ')', 1 },
		{ ']', 2 },
		{ '}', 3 },
		{ '>', 4 }
	};

	public IncompleteSyntaxResult(string completionString)
	{
		CompletionString = completionString;
	}

	public IncompleteSyntaxResult(char[] missingBrackets) : this(new string(missingBrackets)) { }

	public long Score => CompletionString
			.Select(b => ScoreTable[b])
			.Aggregate(0L, (acc, s) => acc * 5 + s);
}
