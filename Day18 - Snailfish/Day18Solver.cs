using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day18;

public class Day18Solver : DaySolver
{
	private readonly IList<Pair> _pairs;

	public Day18Solver(string inputFilePath) : base(inputFilePath)
	{
		_pairs = InputLines.Select(Pair.Parse).ToList();
	}

	public override string SolvePart1()
	{
		Pair sum = _pairs.Sum();
		int result = sum.Magnitude;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int maxMagnitude = int.MinValue;
		for (int i = 0; i < _pairs.Count; i++)
		{
			for (int j = i + 1; j < _pairs.Count; j++)
			{
				Pair pair = _pairs[i] + _pairs[j];
				int magnitude = pair.Magnitude;
				if (magnitude > maxMagnitude)
				{
					maxMagnitude = magnitude;
				}
				pair = _pairs[j] + _pairs[i];
				magnitude = pair.Magnitude;
				if (magnitude > maxMagnitude)
				{
					maxMagnitude = magnitude;
				}
			}
		}
		return maxMagnitude.ToString();
	}
}
