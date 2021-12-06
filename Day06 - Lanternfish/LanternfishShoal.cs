using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode.Year2021.Day06;

public class LanternfishShoal
{
	private BigInteger[] _lanternfishShoalArray;

	public LanternfishShoal(ICollection<Lanternfish> lanternfishShoal)
	{
		_lanternfishShoalArray = new BigInteger[Lanternfish.MaxTimeToNextCycle + 1];
		foreach (Lanternfish fish in lanternfishShoal)
		{
			_lanternfishShoalArray[fish.TimeToNextCycle] += 1;
		}
	}

	public BigInteger Count => _lanternfishShoalArray.Sum();

	public void AdvanceDay()
	{
		BigInteger reproducingFishCount = _lanternfishShoalArray[0];
		// Move down by 1
		Array.Copy(_lanternfishShoalArray, 1, _lanternfishShoalArray, 0, _lanternfishShoalArray.Length - 1);
		_lanternfishShoalArray[^1] = 0;
		// Add newly born fish
		_lanternfishShoalArray[Lanternfish.BirthToReproductionTime - 1] += reproducingFishCount;
		// Begin new reproductive cycle
		_lanternfishShoalArray[Lanternfish.ReproductiveCycleTime - 1] += reproducingFishCount;
	}
}
