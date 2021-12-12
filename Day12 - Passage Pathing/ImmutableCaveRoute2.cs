using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdventOfCode.Year2021.Day12;

/// <summary>
/// Immutable Cave route with an additional small cave visit.
/// All but one small caves can be visited up to one time, and one small cave may be visited up to two times.
/// Big caves may be visited unlimited amount of times.
/// </summary>
public class ImmutableCaveRoute2 : IImmutableCaveRoute
{
	private readonly IImmutableList<Cave> _path;
	private readonly IImmutableSet<Cave> _visitedCaves;

	public IReadOnlyList<Cave> Path => _path;
	public IReadOnlySet<Cave> VisitedCaves => (IReadOnlySet<Cave>)_visitedCaves;
	public Cave? AdditionalSmallCaveVisit { get; private set; }

	public Cave Last => Path[^1];

	public ImmutableCaveRoute2()
	{
		_path = ImmutableList.Create<Cave>();
		_visitedCaves = ImmutableHashSet.Create<Cave>();
		AdditionalSmallCaveVisit = null;
	}

	private ImmutableCaveRoute2(IImmutableList<Cave> path, IImmutableSet<Cave> visitedCaves, Cave? additionalSmallCaveVisit)
	{
		_path = path;
		_visitedCaves = visitedCaves;
		AdditionalSmallCaveVisit = additionalSmallCaveVisit;
	}

	public bool CanExtendTo(Cave cave)
	{
		if (cave.IsBigCave) return true;
		if (!VisitedCaves.Contains(cave)) return true;
		if (AdditionalSmallCaveVisit is null) return true;
		return false;
	}

	public IImmutableCaveRoute ExtendTo(Cave cave)
	{
		IReadOnlyList<Cave> list = ImmutableList.Create<Cave>();
		var newPath = _path.Add(cave);
		var newVisitedCaves = _visitedCaves.Add(cave);
		if (ReferenceEquals(newVisitedCaves, VisitedCaves) && cave.IsSmallCave)
		{
			if (AdditionalSmallCaveVisit is not null)
			{
				throw new InvalidOperationException("Cannot visit two small caves more that once.");
			}
			return new ImmutableCaveRoute2(newPath, newVisitedCaves, cave);
		}
		else
		{
			return new ImmutableCaveRoute2(newPath, newVisitedCaves, AdditionalSmallCaveVisit);
		}
	}
}
