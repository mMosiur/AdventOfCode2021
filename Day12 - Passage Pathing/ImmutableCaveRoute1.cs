using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdventOfCode.Year2021.Day12;

/// <summary>
/// Immutable Cave route.
/// Small caves can be visited up to one time.
/// Big caves may be visited unlimited amount of times.
/// </summary>
public class ImmutableCaveRoute1 : IImmutableCaveRoute
{
	private readonly IImmutableList<Cave> _path;
	private readonly IImmutableSet<Cave> _visitedCaves;

	public IReadOnlyList<Cave> Path => _path;
	public IReadOnlySet<Cave> VisitedCaves => (IReadOnlySet<Cave>)_visitedCaves;

	public Cave Last => Path[^1];

	public ImmutableCaveRoute1()
	{
		_path = ImmutableList.Create<Cave>();
		_visitedCaves = ImmutableHashSet.Create<Cave>();
	}

	private ImmutableCaveRoute1(IImmutableList<Cave> path, IImmutableSet<Cave> visitedCaves)
	{
		_path = path;
		_visitedCaves = visitedCaves;
	}

	public bool CanExtendTo(Cave cave)
	{
		if (cave.IsBigCave) return true;
		if (!VisitedCaves.Contains(cave)) return true;
		return false;
	}

	public IImmutableCaveRoute ExtendTo(Cave cave)
	{
		IReadOnlyList<Cave> list = ImmutableList.Create<Cave>();
		var newPath = _path.Add(cave);
		var newVisitedCaves = _visitedCaves.Add(cave);
		if (ReferenceEquals(newVisitedCaves, VisitedCaves) && cave.IsSmallCave)
		{
			throw new InvalidOperationException("Cannot visit a small cave more that once.");
		}
		return new ImmutableCaveRoute1(newPath, newVisitedCaves);
	}
}
