using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day13;

public class Day13Solver : DaySolver
{
	private readonly IEnumerable<(int, int)> _dotPositions;
	private readonly IEnumerable<FoldInstruction> _instructions;

	public Day13Solver(string inputFilePath) : base(inputFilePath)
	{
		_dotPositions = InputLines
			.TakeWhile(s => !"".Equals(s))
			.Select(s => s.SplitIntoTwo(',', StringSplitOptions.TrimEntries))
			.Select(s => (int.Parse(s.Item1), int.Parse(s.Item2)));
		_instructions = InputLines
			.SkipWhile(s => !"".Equals(s))
			.SkipWhile(s => "".Equals(s))
			.Select(FoldInstruction.Parse);
	}

	public override string SolvePart1()
	{
		PaperFolder paperFolder = PaperFolder.FromDotsPositions(_dotPositions);
		paperFolder.Fold(_instructions.First());
		int result = paperFolder.Count(s => s == PaperSpot.Dot);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		PaperFolder paperFolder = PaperFolder.FromDotsPositions(_dotPositions);
		foreach (FoldInstruction instruction in _instructions)
		{
			paperFolder.Fold(instruction);
		}
		return paperFolder.ToString();
	}
}
