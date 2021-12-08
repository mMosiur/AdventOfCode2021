using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode.Year2021.Day08;

public class SevenSegmentDisplay
{
	public readonly MalfunctioningDisplayDigits _malfunctioningDisplayDigits;

	public int GetDigit(IReadOnlySet<char> digitRepresentation)
	{
		for (int digit = 0; digit <= 9; digit++)
		{
			if (_malfunctioningDisplayDigits[digit].SetEquals(digitRepresentation))
			{
				return digit;
			}
		}
		throw new ArgumentException("Invalid digit representation", nameof(digitRepresentation));
	}

	private readonly IReadOnlySet<char>[] _signalPatterns;
	private readonly IReadOnlySet<char>[] _outputValueDigits;


	public IEnumerable<IReadOnlySet<char>> SignalPatterns => _signalPatterns;
	public IEnumerable<IReadOnlySet<char>> OutputValueDigits => _outputValueDigits;

	public int OutputValue => OutputValueDigits.Aggregate(0, (agg, next) => agg * 10 + GetDigit(next));

	public SevenSegmentDisplay(IEnumerable<string> signalPatterns, IEnumerable<string> outputValues)
	{
		// Check and set signal patterns
		_signalPatterns = signalPatterns.Select(s => ImmutableHashSet.CreateRange(s)).ToArray();
		if (_signalPatterns.Length != 10)
		{
			throw new ArgumentException("There must be exactly 10 signal patterns provided.", nameof(signalPatterns));
		}
		_malfunctioningDisplayDigits = new MalfunctioningDisplayDigits(_signalPatterns);
		// Check and set output values
		_outputValueDigits = outputValues.Select(s => ImmutableHashSet.CreateRange(s)).ToArray();
		if (_outputValueDigits.Length != 4)
		{
			throw new ArgumentException("There must be exactly 4 output values provided.", nameof(outputValues));
		}
	}

	public static SevenSegmentDisplay Parse(string s)
	{
		string[] parts = s.Split('|', StringSplitOptions.TrimEntries);
		if (parts.Length != 2)
		{
			throw new FormatException("Input string was not in a correct format.");
		}
		string[] signalPatterns = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
		string[] outputValues = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
		return new SevenSegmentDisplay(signalPatterns, outputValues);
	}
}
