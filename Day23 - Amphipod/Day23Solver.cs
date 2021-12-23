using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day23;

public class Day23Solver : DaySolver
{
	private readonly AmphipodBurrow _amphipodBurrow1;
	private readonly AmphipodBurrow _amphipodBurrow2;

	public Day23Solver(string inputFilePath) : base(inputFilePath)
	{
		_amphipodBurrow1 = AmphipodBurrow.Parse(InputLines);
		string[] additionalLines = new string[]
		{
			"  #D#C#B#A#",
			"  #D#B#A#C#"
		};
		_amphipodBurrow2 = AmphipodBurrow.Parse(InputLines.Take(3).Concat(additionalLines).Concat(InputLines.Skip(3)));
	}

	private int SolveMinimumEnergy(AmphipodBurrowState start, AmphipodBurrowState goal)
	{
		var dist = new Dictionary<AmphipodBurrowState, int>() { [start] = 0 };
		var visited = new HashSet<AmphipodBurrowState>();
		var queue = new PriorityQueue<AmphipodBurrowState, int>();

		queue.EnqueueRange(dist.Select(x => (x.Key, x.Value)));

		while (queue.Count > 0)
		{
			var current = queue.Dequeue();
			if (!visited.Contains(current))
			{
				visited.Add(current);

				if (current == goal)
					break;

				var currentCost = dist[current];

				foreach (var (next, nextCost) in current.NextPossibleStates())
				{
					var alt = currentCost + nextCost;
					if (alt < dist.GetValueOrDefault(next, int.MaxValue))
					{
						dist[next] = alt;
						queue.Enqueue(next, alt);
					}
				}
			}
		}

		return dist[goal];
	}


	public override string SolvePart1()
	{
		AmphipodBurrowOrganizer organizer = new(_amphipodBurrow1);
		var destinationState = organizer.StartState with { Rooms = string.Concat(organizer.StartState.Rooms.OrderBy(c => c)) };
		int result = organizer.CalculateMinEnergyRequired(destinationState);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		AmphipodBurrowOrganizer organizer = new(_amphipodBurrow2);
		var destinationState = organizer.StartState with { Rooms = string.Concat(organizer.StartState.Rooms.OrderBy(c => c)) };
		int result = organizer.CalculateMinEnergyRequired(destinationState);
		return result.ToString();
	}
}
