using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021.Day13;

public struct FoldInstruction
{
	public FoldDirection Direction { get; init; }
	public int Position { get; init; }

	public static FoldInstruction Parse(string input)
	{
		const string pattern = @"^\s*fold along (?<direction>[xy])\s*=\s*(?<position>\d+)$";
		Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
		Match match = regex.Match(input);
		if (!match.Success)
		{
			throw new FormatException("Invalid input");
		}
		int position = int.Parse(match.Groups["position"].ValueSpan);
		return match.Groups["direction"].Value switch
		{
			"x" => new FoldInstruction { Direction = FoldDirection.Left, Position = position},
			"y" => new FoldInstruction { Direction = FoldDirection.Up, Position = position},
			_ => throw new FormatException("Invalid input")
		};
	}
}
