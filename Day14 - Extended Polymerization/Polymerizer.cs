using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day14;

public class Polymerizer
{
	private readonly IDictionary<(char, char), char> _pairInsertionsRules;
	private readonly IList<char> _polymerTemplate;


	public Polymerizer(IDictionary<(char, char), char> pairInsertionsRules, IList<char> polymerTemplate)
	{
		_pairInsertionsRules = pairInsertionsRules;
		_polymerTemplate = polymerTemplate;
	}

	private Dictionary<(char, char), long> PolymerizationStep(Dictionary<(char, char), long> pairCounts)
	{
		Dictionary<(char, char), long> nextPairCounts = new Dictionary<(char, char), long>();
		foreach (((char, char) pair, long count) in pairCounts)
		{
			if (_pairInsertionsRules.TryGetValue(pair, out char inserted))
			{
				(char, char) nextPair1 = (pair.Item1, inserted);
				nextPairCounts[nextPair1] = checked(nextPairCounts.GetValueOrDefault(nextPair1, 0) + count);
				(char, char) nextPair2 = (inserted, pair.Item2);
				nextPairCounts[nextPair2] = checked(nextPairCounts.GetValueOrDefault(nextPair2, 0) + count);
			}
			else
			{
				nextPairCounts[pair] = nextPairCounts.GetValueOrDefault(pair, 0) + count;
			}
		}
		return nextPairCounts;
	}

	public Dictionary<char, long> ElementOccurrencesAfterCycles(long stepCount)
	{
		if (stepCount < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(stepCount), "Step count must not be negative.");
		}
		Dictionary<(char, char), long> pairCounts = new();
		for (int i = 1; i < _polymerTemplate.Count; i++)
		{
			(char, char) pair = (_polymerTemplate[i - 1], _polymerTemplate[i]);
			if (pairCounts.ContainsKey(pair))
			{
				pairCounts[pair]++;
			}
			else
			{
				pairCounts[pair] = 1;
			}
		}
		for (long i = 0; i < stepCount; i++)
		{
			pairCounts = PolymerizationStep(pairCounts);
		}
		Dictionary<char, long> elementOccurrences = new();
		elementOccurrences[_polymerTemplate[0]] = 1;
		elementOccurrences[_polymerTemplate[^1]] = 1;
		foreach (((char item1, char item2), long count) in pairCounts)
		{
			elementOccurrences[item1] = elementOccurrences.GetValueOrDefault(item1, 0) + count;
			elementOccurrences[item2] = elementOccurrences.GetValueOrDefault(item2, 0) + count;
		}
		foreach ((char element, long count) in elementOccurrences)
		{
			elementOccurrences[element] = count / 2;
		}
		return elementOccurrences;
	}
}
