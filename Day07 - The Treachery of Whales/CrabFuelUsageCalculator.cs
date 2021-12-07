using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day07;

public abstract class CrabFuelUsageCalculator
{
	public abstract int CalculateSingleCrabFuelUsage(int crabPosition, int finalPosition);

	public int CalculateAllCrabsFuelUsage(IList<int> crabPositions, int finalPosition)
	{
		return crabPositions.Select(p => CalculateSingleCrabFuelUsage(p, finalPosition)).Sum();
	}
}
