using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day09;

public class Heightmap
{
	private readonly int[,] _heightmap;
	private readonly int _maxHeight;

	public Heightmap(int[,] heightmap)
	{
		_heightmap = heightmap;
		_maxHeight = _heightmap.Cast<int>().Max();
	}

	public Heightmap(int[,] heightmap, int maxHeight)
	{
		_heightmap = heightmap;
		_maxHeight = maxHeight;
	}

	public IEnumerable<int> LowPointHeights
	{
		get
		{
			for (int row = 0; row < _heightmap.GetLength(0); row++)
			{
				for (int col = 0; col < _heightmap.GetLength(1); col++)
				{
					int height = _heightmap[row, col];
					if (height >= this[row, col - 1]) continue;
					if (height >= this[row, col + 1]) continue;
					if (height >= this[row - 1, col]) continue;
					if (height >= this[row + 1, col]) continue;
					yield return height;
				}
			}
		}
	}

	public IEnumerable<int> LowPointRiskLevels => LowPointHeights.Select(height => 1 + height);


	private int FloodBasin(int[,] heightmap, int row, int col)
	{
		int basinSize = 1;
		heightmap[row, col] = _maxHeight;
		if (heightmap.GetOrDefault(row - 1, col, _maxHeight) < _maxHeight)
		{
			basinSize += FloodBasin(heightmap, row - 1, col);
		}
		if (heightmap.GetOrDefault(row + 1, col, _maxHeight) < _maxHeight)
		{
			basinSize += FloodBasin(heightmap, row + 1, col);
		}
		if (heightmap.GetOrDefault(row, col - 1, _maxHeight) < _maxHeight)
		{
			basinSize += FloodBasin(heightmap, row, col - 1);
		}
		if (heightmap.GetOrDefault(row, col + 1, _maxHeight) < _maxHeight)
		{
			basinSize += FloodBasin(heightmap, row, col + 1);
		}
		return basinSize;
	}

	public IEnumerable<int> BasinSizes
	{
		get
		{
			int[,] heightmap = (int[,])_heightmap.Clone();
			for (int row = 0; row < heightmap.GetLength(0); row++)
			{
				for (int col = 0; col < heightmap.GetLength(1); col++)
				{
					if (heightmap[row, col] == _maxHeight) continue;
					int basinSize = FloodBasin(heightmap, row, col);
					yield return basinSize;
				}
			}
		}
	}

	public int this[int row, int col] => _heightmap.GetOrDefault(row, col, _maxHeight);

	public static Heightmap FromLines(IEnumerable<string> lines, int maxHeight)
	{
		string[] linesArray = lines.ToArray();
		int gridHeight = linesArray.Length;
		int gridWidth = linesArray.First().Length;
		if (!linesArray.All(line => line.Length == gridWidth))
		{
			throw new FormatException("Input is not rectangular");
		}
		int[,] heightmap = new int[gridHeight, gridWidth];
		for (int row = 0; row < gridHeight; row++)
		{
			for (int col = 0; col < gridWidth; col++)
			{
				int height = (int)char.GetNumericValue(linesArray[row][col]);
				heightmap[row, col] = height;
			}
		}
		return new Heightmap(heightmap, maxHeight);
	}
}
