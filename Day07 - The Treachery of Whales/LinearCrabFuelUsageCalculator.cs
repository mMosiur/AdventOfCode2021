using System;

namespace AdventOfCode.Year2021.Day07;

public class LinearCrabFuelUsageCalculator : CrabFuelUsageCalculator
{
	public override int CalculateSingleCrabFuelUsage(int crabPosition, int finalPosition)
	{
		int distance = Math.Abs(finalPosition - crabPosition);
		return (distance * (distance + 1)) / 2;
	}
}
