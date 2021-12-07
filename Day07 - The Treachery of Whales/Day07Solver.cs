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

	private int CalculateConstantFuelUsage(int startingPosition, int finalPosition)
	{
		int difference = Math.Abs(finalPosition - startingPosition);
		return difference;
	}

	private int CalculateLinearFuelUsage(int startingPosition, int finalPosition)
	{
		int difference = Math.Abs(finalPosition - startingPosition);
		return (difference * (difference + 1)) / 2;
	}

	private int SmallestFuelUsage(CrabFuelUsageCalculator calculator)
	{
		int minPosition = _numbers.Min();
		int maxPosition = _numbers.Max();
		int result = Enumerable
			.Range(minPosition, maxPosition - minPosition + 1)
			.Select(n => calculator.CalculateAllCrabsFuelUsage(_numbers, n))
			.Min();
		return result;
	}

	public override string SolvePart1()
	{
		CrabFuelUsageCalculator calculator = new ConstantCrabFuelUsageCalculator();
		int result = SmallestFuelUsage(calculator);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		CrabFuelUsageCalculator calculator = new LinearCrabFuelUsageCalculator();
		int result = SmallestFuelUsage(calculator);
		return result.ToString();
	}
}
