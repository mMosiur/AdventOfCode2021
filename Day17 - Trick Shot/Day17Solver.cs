using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day17;

public class Day17Solver : DaySolver
{
	private readonly Target _target;

	public Day17Solver(string inputFilePath) : base(inputFilePath)
	{
		_target = Target.Parse(Input);
	}

	public override string SolvePart1()
	{
		TrajectoryCalculator calculator = new();
		int result = calculator.HighestPossibleVerticalPosition(_target);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		TrajectoryCalculator calculator = new();
		int result = calculator.InitialVelocitiesThatHitTarget(_target).Count();
		return result.ToString();
	}
}
