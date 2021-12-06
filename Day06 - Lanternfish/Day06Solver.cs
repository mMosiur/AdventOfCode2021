using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day06;

public class Day06Solver : DaySolver
{
	private ICollection<Lanternfish> _initialLanternfish;

	public Day06Solver(string inputFilePath) : base(inputFilePath)
	{
		_initialLanternfish = InputLines.Single()
			.Split(',', StringSplitOptions.TrimEntries)
			.Select(int.Parse)
			.Select(i => new Lanternfish(i))
			.ToList();
	}

	private BigInteger FishCountAfter(int days)
	{
		LanternfishShoal lanternfishShoal = new(_initialLanternfish);
		for (int i = 0; i < days; i++)
		{
			lanternfishShoal.AdvanceDay();
		}
		return lanternfishShoal.Count;
	}

	public override string SolvePart1()
	{
		const int DAYS = 80;
		BigInteger result = FishCountAfter(DAYS);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int DAYS = 256;
		BigInteger result = FishCountAfter(DAYS);
		return result.ToString();
	}
}
