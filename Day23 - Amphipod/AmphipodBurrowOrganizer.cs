using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day23;

public class AmphipodBurrowOrganizer
{
	private AmphipodBurrow _amphipodBurrow;

	public AmphipodBurrowState StartState { get; }

	public AmphipodBurrowOrganizer(AmphipodBurrow amphipodBurrow)
	{
		_amphipodBurrow = amphipodBurrow;
		StartState = _amphipodBurrow.State;
	}

	public int CalculateMinEnergyRequired(AmphipodBurrowState destinationState)
	{
		Dictionary<AmphipodBurrowState, int> cache = new() { [StartState] = 0 };
		HashSet<AmphipodBurrowState> visitedStates = new();
		PriorityQueue<AmphipodBurrowState, int> queue = new();
		queue.Enqueue(StartState, 0);

		while (queue.TryDequeue(out var current, out int currentTotalEnergy))
		{
			if (!visitedStates.Contains(current))
			{
				if (current.Equals(destinationState))
				{
					return currentTotalEnergy;
				}
				foreach ((AmphipodBurrowState nextState, int energy) in current.NextPossibleStates())
				{
					int nextTotalEnergy = currentTotalEnergy + energy;
					if (!cache.TryGetValue(nextState, out int previousTotalEnergy) || nextTotalEnergy < previousTotalEnergy)
					{
						cache[nextState] = nextTotalEnergy;
						queue.Enqueue(nextState, nextTotalEnergy);
					}
				}
				visitedStates.Add(current);
			}
		}
		throw new InvalidOperationException("No organization resulting in given destination state found.");
	}
}
