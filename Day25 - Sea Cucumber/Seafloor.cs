using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day25;

public class Seafloor
{
	private SeaCucumber?[,] _seafloor;

	public Seafloor(SeaCucumber?[,] seafloor)
	{
		_seafloor = seafloor;
	}

	private int MoveIf(Predicate<SeaCucumber> shouldTryToMove)
	{
		int height = _seafloor.GetLength(0);
		int width = _seafloor.GetLength(1);
		SeaCucumber?[,] newSeafloor = (SeaCucumber?[,])_seafloor.Clone();
		int movedCount = 0;
		// Move east-facing cucumbers
		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				SeaCucumber? seaCucumber = _seafloor[row, col];
				if (seaCucumber is null) continue;
				if (!shouldTryToMove.Invoke(seaCucumber)) continue;
				int destinationRow = (row + seaCucumber.Direction.Row) % height;
				int destinationCol = (col + seaCucumber.Direction.Column) % width;
				if (_seafloor[destinationRow, destinationCol] is not null) continue;
				newSeafloor[row, col] = null;
				newSeafloor[destinationRow, destinationCol] = seaCucumber;
				movedCount++;
			}
		}
		_seafloor = newSeafloor;
		return movedCount;
	}

	public int Step()
	{
		int height = _seafloor.GetLength(0);
		int width = _seafloor.GetLength(1);
		SeaCucumber?[,] newSeafloor = (SeaCucumber?[,])_seafloor.Clone();
		int movedCount = 0;
		// Move east-facing cucumbers
		movedCount += MoveIf(seaCucumber => seaCucumber.IsEastFacing());
		// Move south-facing cucumbers
		movedCount += MoveIf(seaCucumber => seaCucumber.IsSoutFacing());
		return movedCount;
	}

	public static Seafloor Parse(IEnumerable<string> inputLines)
	{
		string[] lines = inputLines.ToArray();
		int height = lines.Length;
		int width = lines[0].Length;
		if (!lines.All(line => line.Length == width))
		{
			throw new FormatException("All lines must have the same length.");
		}
		SeaCucumber?[,] seafloor = new SeaCucumber?[height, width];
		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				char c = lines[row][col];
				seafloor[row, col] = c == '.' ? null : SeaCucumber.FromChar(c, new Point(row, col));
			}
		}
		return new Seafloor(seafloor);
	}
}
