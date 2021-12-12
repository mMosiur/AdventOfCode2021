using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Year2021.Day12;

public class CaveSystem
{
	private ISet<Cave> _caves;

	public IReadOnlySet<Cave> Caves => (IReadOnlySet<Cave>)_caves;
	public Cave Start { get; }
	public Cave End { get; }

	public CaveSystem(ISet<Cave> caves, Cave start, Cave end)
	{
		if (!caves.Contains(start) || !caves.Contains(end))
		{
			throw new ArgumentException("Start and end caves must be in the cave system.");
		}
		if (!caves.All(c => c.ConnectedCaves.All(c2 => caves.Contains(c2))))
		{
			throw new ArgumentException("All caves must be connected.");
		}
		_caves = caves;
		Start = start;
		End = end;
	}

	public IEnumerable<IImmutableCaveRoute> FindRoutes<T>()
		where T : IImmutableCaveRoute, new()
	{
		IImmutableCaveRoute emptyRoute = new T();
		Queue<IImmutableCaveRoute> queue = new Queue<IImmutableCaveRoute>();
		queue.Enqueue(emptyRoute.ExtendTo(Start));
		while (queue.TryDequeue(out IImmutableCaveRoute? route))
		{
			foreach (Cave nextCave in route.Last.ConnectedCaves)
			{
				if (ReferenceEquals(nextCave, Start)) continue;
				if (ReferenceEquals(nextCave, End))
				{
					yield return route.ExtendTo(nextCave);
					continue;
				}
				if (!route.CanExtendTo(nextCave)) continue;
				queue.Enqueue(route.ExtendTo(nextCave));
			}
		}
	}
}
