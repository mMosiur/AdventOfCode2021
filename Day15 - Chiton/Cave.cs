using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day15;

public class Cave
{
	private readonly CaveSegment[,] _cave;

	private CaveSegment _start { get; }
	private CaveSegment _end { get; }

	public Cave(CaveSegment[,] segments)
	{
		_cave = segments;
		_start = segments[0, 0];
		_end = segments[segments.GetLength(0) - 1, segments.GetLength(1) - 1];
	}

	public int LowestTotalRiskLevel()
	{
		PriorityQueue<CaveSegment, int> queue = new();
		Dictionary<CaveSegment, int> visited = new();
		queue.Enqueue(_start, 0);
		visited[_start] = 0;
		while (queue.TryDequeue(out CaveSegment? caveSegment, out int currentTotalRiskLevel))
		{
			if (ReferenceEquals(caveSegment, _end))
			{
				return currentTotalRiskLevel;
			}
			foreach (CaveSegment adjacentSegment in caveSegment.AdjacentSegments)
			{
				int newTotalRiskLevel = currentTotalRiskLevel + adjacentSegment.RiskLevel;
				if (!visited.TryGetValue(adjacentSegment, out int visitedDistance) || newTotalRiskLevel < visitedDistance)
				{
					visited[adjacentSegment] = newTotalRiskLevel;
					queue.Enqueue(adjacentSegment, currentTotalRiskLevel + adjacentSegment.RiskLevel);
				}
			}
		}
		throw new InvalidOperationException("No path from start to end found.");
	}

	public static Cave FromRiskLevelMap(int[,] riskLevelMap)
	{
		return FromRiskLevelTile(riskLevelMap, 1, 1);
	}

	public static Cave FromRiskLevelTile(int[,] riskLevelTile, int verticalTileCount, int horizontalTileCount)
	{
		int tileHeight = riskLevelTile.GetLength(0);
		int height = tileHeight * verticalTileCount;
		int tileWidth = riskLevelTile.GetLength(1);
		int width = tileWidth * horizontalTileCount;
		CaveSegment[,] cave = new CaveSegment[height, width];
		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				int tileRowOffset = row / tileHeight;
				int rowInTile = row % tileHeight;
				int tileColOffset = col / tileWidth;
				int colInTile = col % tileWidth;
				int riskLevel = riskLevelTile[rowInTile, colInTile] + tileRowOffset + tileColOffset;
				while (riskLevel > 9) riskLevel -= 9;
				cave[row, col] = new CaveSegment(riskLevel);
				int topRow = row - 1;
				if (topRow >= 0)
				{
					cave[row, col].AdjacentSegments.Add(cave[topRow, col]);
					cave[topRow, col].AdjacentSegments.Add(cave[row, col]);
				}
				int leftCol = col - 1;
				if (leftCol >= 0)
				{
					cave[row, col].AdjacentSegments.Add(cave[row, leftCol]);
					cave[row, leftCol].AdjacentSegments.Add(cave[row, col]);
				}
			}
		}
		return new Cave(cave);
	}
}
