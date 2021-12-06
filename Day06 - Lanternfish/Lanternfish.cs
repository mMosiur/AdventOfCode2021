using System;

namespace AdventOfCode.Year2021.Day06;

public struct Lanternfish
{
	public static readonly int ReproductiveCycleTime = 7;
	public static readonly int BirthToReproductionTime = ReproductiveCycleTime + 2;
	public static int MaxTimeToNextCycle => Math.Max(ReproductiveCycleTime, BirthToReproductionTime);

	public int TimeToNextCycle { get; set; }

	public Lanternfish(int timeToNextCycle)
	{
		TimeToNextCycle = timeToNextCycle;
	}
}
