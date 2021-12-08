using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day07;

public class Day07Solver : DaySolver
{
	private IList<int> _numbers;

	public Day07Solver(string inputFilePath) : base(inputFilePath)
	{
		_numbers = Input.Split(',').Select(int.Parse).ToList();
	}

	private int SmallestFuelUsage(CrabFuelUsageCalculator calculator, int minPosition, int maxPosition)
	{
		return Enumerable
			.Range(minPosition, maxPosition - minPosition + 1)
			.Select(n => calculator.CalculateAllCrabsFuelUsage(_numbers, n))
			.Min();
	}

	public override string SolvePart1()
	{
		CrabFuelUsageCalculator calculator = new ConstantCrabFuelUsageCalculator();
		// Excact answer is the median of positions.
		double median = Statistics.Median(_numbers.Select(n => (double)n));
		int minPosition = (int)Math.Floor(median);
		int maxPosition = (int)Math.Ceiling(median);
		int result = SmallestFuelUsage(calculator, minPosition, maxPosition);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		CrabFuelUsageCalculator calculator = new LinearCrabFuelUsageCalculator();
		// The answer is bounded by [1/2 - k; 1/2 + k] where k is the mean of positions.
		// As per https://cdn.discordapp.com/attachments/541932275068174359/917882191298592788/crab_submarines.pdf
		double avarage = _numbers.Average();
		int minPosition = (int)Math.Floor(avarage - 0.5);
		int maxPosition = (int)Math.Ceiling(avarage + 0.5);
		int result = SmallestFuelUsage(calculator, minPosition, maxPosition);
		return result.ToString();
	}
}
