using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day01;

public class Day01Solver : DaySolver
{
	private IReadOnlyList<int> _numbers;

	public Day01Solver(string inputFilePath) : base(inputFilePath)
	{
		_numbers = InputLines.Select(int.Parse).ToList();
	}

	public override string SolvePart1()
	{
		int count = 0;
		int prev = _numbers[0];
		for (int i = 1; i < _numbers.Count; i++)
		{
			int current = _numbers[i];
			count += current > prev ? 1 : 0;
			prev = current;
		}
		return count.ToString();
	}

	public override string SolvePart2()
	{
		int count = 0;
		int sumA = _numbers.Take(3).Sum();
		int sumB = sumA;
		for (int i = 3; i < _numbers.Count; i++)
		{
			sumB -= _numbers[i - 3];
			sumB += _numbers[i];
			count += sumB > sumA ? 1 : 0;
			sumA = sumB;
		}
		return count.ToString();
	}
}
