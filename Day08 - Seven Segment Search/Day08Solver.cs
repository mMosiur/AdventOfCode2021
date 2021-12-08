using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day08;

public class Day08Solver : DaySolver
{
	ICollection<SevenSegmentDisplay> _sevenSegmentDisplays;

	public Day08Solver(string inputFilePath) : base(inputFilePath)
	{
		_sevenSegmentDisplays = InputLines.Select(SevenSegmentDisplay.Parse).ToList();
	}

	public override string SolvePart1()
	{
		int[] numbers = { 2, 4, 3, 7 };
		int result = _sevenSegmentDisplays.Sum(display =>
			display.OutputValueDigits.Count(digit =>
				numbers.Contains(digit.Count)
			)
		);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = _sevenSegmentDisplays.Sum(display => display.OutputValue);
		return result.ToString();
	}
}
