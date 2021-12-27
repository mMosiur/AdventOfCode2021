using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day25;

public class Day25Solver : DaySolver
{
	private readonly Seafloor _seafloor;

	public Day25Solver(string inputFilePath) : base(inputFilePath)
	{
		_seafloor = Seafloor.Parse(InputLines);
	}

	public override string SolvePart1()
	{
		int steps = 0;
		while (true)
		{
			int seaCucumbersMoves = _seafloor.Step();
			steps++;
			if (seaCucumbersMoves == 0) break;
		}
		return steps.ToString();
	}

	public override string SolvePart2()
	{
		return string.Empty;
	}
}
