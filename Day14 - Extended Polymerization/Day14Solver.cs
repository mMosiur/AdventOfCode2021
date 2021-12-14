using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day14;

public class Day14Solver : DaySolver
{
	private readonly Polymerizer _polymerizer;

	public Day14Solver(string inputFilePath) : base(inputFilePath)
	{
		var it = InputLines.GetEnumerator();
		Dictionary<(char, char), char> pairInsertionRules = new Dictionary<(char, char), char>();
		it.MoveNext();
		string polymerTemplate = it.Current;
		while (it.MoveNext() && "".Equals(it.Current)) { }
		while (!"".Equals(it.Current))
		{
			string[] parts = it.Current.Split("->", StringSplitOptions.TrimEntries);
			if (parts.Length != 2)
			{
				throw new FormatException();
			}
			if (parts[0].Length != 2)
			{
				throw new FormatException();
			}
			if (parts[1].Length != 1)
			{
				throw new FormatException();
			}
			pairInsertionRules.Add((parts[0][0], parts[0][1]), parts[1][0]);
			if (!it.MoveNext()) break;
		}
		_polymerizer = new Polymerizer(pairInsertionRules, polymerTemplate.ToCharArray());
	}

	public override string SolvePart1()
	{
		const int NumberOfCycles = 10;
		var occurrences = _polymerizer.ElementOccurrencesAfterCycles(NumberOfCycles);
		long min = occurrences.Values.Min();
		long max = occurrences.Values.Max();
		long result = max - min;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int NumberOfCycles = 40;
		var occurrences = _polymerizer.ElementOccurrencesAfterCycles(NumberOfCycles);
		long min = occurrences.Values.Min();
		long max = occurrences.Values.Max();
		long result = max - min;
		return result.ToString();
	}
}
