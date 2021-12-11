namespace AdventOfCode.Year2021.Day11;

public class OctopiGrid
{
	private DumboOctopus[,] _octopuses;

	public int FlashCount { get; private set; } = 0;

	public int Count => _octopuses.Length;

	public OctopiGrid(int[,] energyLevels)
	{
		_octopuses = new DumboOctopus[energyLevels.GetLength(0), energyLevels.GetLength(1)];
		for (int x = 0; x < energyLevels.GetLength(0); x++)
		{
			for (int y = 0; y < energyLevels.GetLength(1); y++)
			{
				_octopuses[x, y] = new DumboOctopus(energyLevels[x, y]);
			}
		}
	}

	private int FlashFrom(int row, int col)
	{
		int flashed = 1;
		for (int dRow = -1; dRow <= 1; dRow++)
		{
			for (int dCol = -1; dCol <= 1; dCol++)
			{
				if ((dRow | dCol) == 0) continue;
				int newRow = row + dRow;
				int newCol = col + dCol;
				if (!_octopuses.IndexInRange(newRow, newCol)) continue;
				DumboOctopus octopus = _octopuses[newRow, newCol];
				if (octopus.Step())
				{
					flashed += FlashFrom(newRow, newCol);
				}
			}
		}
		return flashed;
	}

	public int Step()
	{
		int flashed = 0;
		for (int row = 0; row < _octopuses.GetLength(0); row++)
		{
			for (int col = 0; col < _octopuses.GetLength(1); col++)
			{
				DumboOctopus octopus = _octopuses[row, col];
				if (octopus.Step())
				{
					flashed += FlashFrom(row, col);
				}
			}
		}
		foreach (DumboOctopus octopus in _octopuses)
		{
			octopus.ClearFlash();
		}
		FlashCount += flashed;
		return flashed;
	}
}
