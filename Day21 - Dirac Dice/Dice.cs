using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day21;

public class DeterministicDie
{
	private int _previousValue = 0;

	public int RollCount { get; private set; } = 0;

	public int Sides { get; }

	public DeterministicDie(int sides)
	{
		Sides = sides;
	}

	public int Roll()
	{
		RollCount++;
		_previousValue = (_previousValue % Sides) + 1;
		return _previousValue;
	}
}

public class DiracDie
{
	public int Sides { get; }

	public DiracDie(int sides)
	{
		Sides = sides;
	}

	public IEnumerable<int> Roll()
	{
		return Enumerable.Range(1, Sides);
	}
}
