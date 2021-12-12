using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day12;

public class Day12Solver : DaySolver
{
	private CaveSystem _caveSystem;

	public Day12Solver(string inputFilePath) : base(inputFilePath)
	{
		Dictionary<string, Cave> caves = new();
		foreach (string line in InputLines)
		{
			string[] parts = line.Split('-', StringSplitOptions.TrimEntries);
			if (parts.Length != 2)
			{
				throw new InvalidDataException($"Invalid line \"{line}\" in the input file.");
			}
			if (!caves.TryGetValue(parts[0], out Cave? cave1))
			{
				cave1 = new Cave(parts[0]);
				caves.Add(parts[0], cave1);
			}
			if (!caves.TryGetValue(parts[1], out Cave? cave2))
			{
				cave2 = new Cave(parts[1]);
				caves.Add(parts[1], cave2);
			}
			cave1.ConnectedCaves.Add(cave2);
			cave2.ConnectedCaves.Add(cave1);
		}
		Cave startCave = caves.GetValueOrDefault("start")
			?? throw new InvalidDataException($"The input file does not contain a start cave.");
		Cave endCave = caves.GetValueOrDefault("end")
			?? throw new InvalidDataException($"The input file does not contain an end cave.");
		_caveSystem = new CaveSystem(caves.Values.ToHashSet(), startCave, endCave);
	}

	public override string SolvePart1()
	{
		IEnumerable<IImmutableCaveRoute> routes = _caveSystem.FindRoutes<ImmutableCaveRoute1>();
		int result = routes.Count();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		IEnumerable<IImmutableCaveRoute> routes = _caveSystem.FindRoutes<ImmutableCaveRoute2>();
		int result = routes.Count();
		return result.ToString();
	}
}
