using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day10;

public class SyntaxChecker
{
	public static readonly IReadOnlyDictionary<char, char> BracketPairs = new Dictionary<char, char>
	{
		{'(', ')'},
		{'[', ']'},
		{'{', '}'},
		{'<', '>'}
	};

	public ISyntaxResult Check(string line)
	{
		Stack<char> openings = new Stack<char>();
		foreach (char bracket in line)
		{
			if (IsOpeningBracket(bracket))
			{
				openings.Push(bracket);
			}
			else if (!IsClosingBracket(bracket) || !openings.TryPop(out char opening) || !Match(opening, bracket))
			{
				return new CorruptedSyntaxResult(bracket);
			}
		}
		if (openings.Count > 0)
		{
			List<char> missingBrackets = new(openings.Count);
			while (openings.TryPop(out char opening))
			{
				missingBrackets.Add(BracketPairs[opening]);
			}
			return new IncompleteSyntaxResult(missingBrackets.ToArray());
		}
		return new CorrectSyntaxResult();
	}

	public static bool Match(char opening, char closing) => BracketPairs[opening] == closing;

	public static bool IsOpeningBracket(char bracket) => BracketPairs.ContainsKey(bracket);

	public static bool IsClosingBracket(char bracket) => BracketPairs.Values.Contains(bracket);
}
