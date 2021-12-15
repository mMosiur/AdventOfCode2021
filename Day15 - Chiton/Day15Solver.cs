using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day15;

public class Day15Solver : DaySolver
{
	private readonly int[,] _riskLevelMap;

	public Day15Solver(string inputFilePath) : base(inputFilePath)
	{
		string[] lines = InputLines.ToArray();
		int height = lines.Length;
		int width = lines[0].Length;
		if (!lines.All(line => line.Length == width))
		{
			throw new FormatException();
		}
		_riskLevelMap = new int[height, width];
		for (int row = 0; row < lines.Length; row++)
		{
			for (int col = 0; col < lines[row].Length; col++)
			{
				int riskLevel = (int)char.GetNumericValue(lines[row][col]);
				_riskLevelMap[row, col] = riskLevel;
			}
		}
	}

	public override string SolvePart1()
	{
		Cave cave = Cave.FromRiskLevelMap(_riskLevelMap);
		int result = cave.LowestTotalRiskLevel();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int VerticalTileCount = 5;
		const int HorizontalTileCount = 5;
		Cave cave = Cave.FromRiskLevelTile(_riskLevelMap, VerticalTileCount, HorizontalTileCount);
		int result = cave.LowestTotalRiskLevel();
		return result.ToString();
	}
}
