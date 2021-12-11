using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day11;

public class Day11Solver : DaySolver
{
	private int[,] _energyLevels;

	public Day11Solver(string inputFilePath) : base(inputFilePath)
	{
		string[] lines = InputLines.ToArray();
		int height = lines.Length;
		int width = lines[0].Length;
		if (!lines.All(line => line.Length == width))
		{
			throw new FormatException("Input is not rectangular");
		}
		_energyLevels = new int[height, width];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				_energyLevels[y, x] = (int)char.GetNumericValue(lines[y][x]);
			}
		}
	}

	public override string SolvePart1()
	{
		OctopiGrid octopiGrid = new OctopiGrid(_energyLevels);
		for (int i = 0; i < 100; i++)
		{
			octopiGrid.Step();
		}
		return octopiGrid.FlashCount.ToString();
	}

	public override string SolvePart2()
	{
		OctopiGrid octopiGrid = new OctopiGrid(_energyLevels);
		int step = 0;
		while (true)
		{
			step += 1;
			int flashed = octopiGrid.Step();
			if (flashed == octopiGrid.Count)
			{
				return step.ToString();
			}
		}
	}
}
