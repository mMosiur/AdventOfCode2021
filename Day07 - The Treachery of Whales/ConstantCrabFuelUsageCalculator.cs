using System;

namespace AdventOfCode.Year2021.Day07;

public class ConstantCrabFuelUsageCalculator : CrabFuelUsageCalculator
{
	public override int CalculateSingleCrabFuelUsage(int crabPosition, int finalPosition)
	{
		return Math.Abs(finalPosition - crabPosition);
	}
}
