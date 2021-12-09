using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day09;

public class Day09Solver : DaySolver
{
	private readonly Heightmap _heightmap;

	public Day09Solver(string inputFilePath) : base(inputFilePath)
	{
		const int MaxHeight = 9;
		_heightmap = Heightmap.FromLines(InputLines, MaxHeight);
	}

	public override string SolvePart1()
	{
		// The sum of the risk levels of all low points in the heightmap
		int result = _heightmap.LowPointRiskLevels.Sum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		// Product of the sizes of the three largest basins
		int result = _heightmap.BasinSizes
			.OrderByDescending(s => s)
			.Take(3)
			.Aggregate((agg, next) => agg * next);
		return result.ToString();
	}
}

