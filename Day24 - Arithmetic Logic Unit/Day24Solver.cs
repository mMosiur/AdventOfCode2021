using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day24;

public class Day24Solver : DaySolver
{
	private readonly int[] _xAddend = new int[14];
	private readonly int[] _yAddend = new int[14];
	private readonly int[] _zDivisor = new int[14];

	public Day24Solver(string inputFilePath) : base(inputFilePath)
	{
		const string pattern = @"inp w\nmul x 0\nadd x z\nmod x 26\ndiv z (-?\d+)\nadd x (-?\d+)\neql x w\neql x 0\nmul y 0\nadd y 25\nmul y x\nadd y 1\nmul z y\nmul y 0\nadd y w\nadd y (-?\d+)\nmul y x\nadd z y";
		Regex regex = new Regex(pattern);
		var matches = regex.Matches(Input);
		if (matches.Count != 14)
		{
			throw new FormatException("Invalid input");
		}
		if (!matches.All(m => m.Groups.Count == 4))
		{
			throw new FormatException("Invalid input");
		}
		for (int i = 0; i < 14; i++)
		{
			_zDivisor[i] = int.Parse(matches[i].Groups[1].ValueSpan);
			_xAddend[i] = int.Parse(matches[i].Groups[2].ValueSpan);
			_yAddend[i] = int.Parse(matches[i].Groups[3].ValueSpan);
		}
	}

	private IEnumerable<int> BackwardRun(int xAddend, int yAddend, int zDivisor, int prevZ, int partialSum)
	{
		int x = prevZ - partialSum - yAddend;
		if (x % 26 == 0)
		{
			yield return (x / 26) * zDivisor;
		}
		int diff = partialSum - xAddend;
		if (0 <= diff && diff < 26)
		{
			yield return diff + prevZ * zDivisor;
		}
	}

	private string FullRun(IEnumerable<int> partialSumOrder)
	{
		HashSet<int> zValues = new() { 0 };
		Dictionary<int, string> result = new();
		foreach ((int xAddend, int yAddend, int zDivisor) in Enumerable.Zip(_xAddend.Reverse(), _yAddend.Reverse(), _zDivisor.Reverse()))
		{
			HashSet<int> zValuesNext = new();
			foreach (int partialSum in partialSumOrder)
			{
				foreach (int zValue in zValues)
				{
					foreach (int zNext in BackwardRun(xAddend, yAddend, zDivisor, zValue, partialSum))
					{
						zValuesNext.Add(zNext);
						result[zNext] = $"{partialSum}{result.GetValueOrDefault(zValue)}";
					}
				}
			}
			zValues = zValuesNext;
		}
		return result[0];
	}

	public override string SolvePart1()
	{
		IEnumerable<int> partialSumOrder = Enumerable.Range(1, 9);
		return FullRun(partialSumOrder);
	}

	public override string SolvePart2()
	{
		IEnumerable<int> partialSumOrder = Enumerable.Range(1, 9).Select(d => 10 - d);
		return FullRun(partialSumOrder);
	}
}
