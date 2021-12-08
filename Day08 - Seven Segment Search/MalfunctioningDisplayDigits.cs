using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day08;

public class MalfunctioningDisplayDigits
{
	private readonly ICollection<IReadOnlySet<char>> _signalPatterns;
	private readonly IReadOnlySet<char>?[] _representations = new IReadOnlySet<char>?[10];
	private readonly IDictionary<char, char> _segments = new Dictionary<char, char>();
	private IReadOnlySet<char>? _horizontalSegments;

	private IReadOnlySet<char> HorizontalSegments
	{
		get => _horizontalSegments ??= _signalPatterns
			.Where(s => s.Count == 5)
			.Aggregate((agg, next) => agg.Intersect(next).ToHashSet());
	}

	public IReadOnlyDictionary<char, char> Segments => (IReadOnlyDictionary<char, char>)_segments;

	public MalfunctioningDisplayDigits(ICollection<IReadOnlySet<char>> signalPatterns)
	{
		if (signalPatterns.Count != 10)
		{
			throw new ArgumentException("Signal patterns must contain 10 patterns", nameof(signalPatterns));
		}
		_signalPatterns = signalPatterns;
	}

	private IReadOnlySet<char> CalculateTwoRepresentation()
	{
		HashSet<char> sixCountsSymmetricExcept = this[8].ToHashSet();
		foreach (IReadOnlySet<char> digit in _signalPatterns.Where(s => s.Count == 6))
		{
			sixCountsSymmetricExcept.SymmetricExceptWith(digit);
		}
		return sixCountsSymmetricExcept.Union(this['a'], this['g']).ToHashSet();
	}

	public char this[char segment]
	{
		get
		{
			if (!_segments.TryGetValue(segment, out var value))
			{
				value = segment switch
				{
					'a' => this[7].Except(this[1]).Single(),
					'b' => this[9].Except(this[3]).Single(),
					'c' => this[2].Intersect(this[1]).Single(),
					'd' => HorizontalSegments.Except(this['a'], this['g']).Single(),
					'e' => this[0].Except(this[4]).Except(this['a'], this['g']).Single(),
					'f' => this[1].Except(this[2]).Single(),
					'g' => HorizontalSegments.Except(this[4]).Except(this[7]).Single(),
					_ => throw new ArgumentException($"Invalid segment '{segment}'", nameof(segment))
				};
				_segments.Add(segment, value);
			}
			return value;
		}
	}

	public IReadOnlySet<char> this[int digit]
	{
		get
		{
			if (digit < 0 || digit > 9)
			{
				throw new IndexOutOfRangeException($"Digit must be between 0 and 9, inclusive. Was \"{digit}\".");
			}
			return _representations[digit] ??= digit switch
			{
				0 => this[8].Except(this['d']).ToHashSet(),
				1 => _signalPatterns.Single(p => p.Count == 2),
				2 => CalculateTwoRepresentation(),
				3 => this[1].Union(HorizontalSegments).ToHashSet(),
				4 => _signalPatterns.Single(p => p.Count == 4),
				5 => this[6].Except(this['e']).ToHashSet(),
				6 => this[8].Except(this['c']).ToHashSet(),
				7 => _signalPatterns.Single(p => p.Count == 3),
				8 => _signalPatterns.Single(p => p.Count == 7),
				9 => this[8].Except(this['e']).ToHashSet(),
				_ => throw new IndexOutOfRangeException($"Digit must be between 0 and 9, inclusive. Was \"{digit}\".")
			};
		}
	}
}
