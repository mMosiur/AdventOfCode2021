using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day12;

public interface IImmutableCaveRoute
{
	public IReadOnlyList<Cave> Path { get; }
	public IReadOnlySet<Cave> VisitedCaves { get; }

	public Cave Last { get; }

	public bool CanExtendTo(Cave cave);
	public IImmutableCaveRoute ExtendTo(Cave cave);
}
