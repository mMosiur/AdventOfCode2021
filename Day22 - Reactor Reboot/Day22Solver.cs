using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day22;

public class Day22Solver : DaySolver
{
	private IList<ReactorCoreRebootStep> _steps;
	private ReactorCore _core;
	bool rebooted = false;

	public Day22Solver(string inputFilePath) : base(inputFilePath)
	{
		_steps = InputLines.Select(ReactorCoreRebootStep.Parse).ToList();
		_core = new();
	}

	public override string SolvePart1()
	{
		const int InitializationRangeStart = -50;
		const int InitializationRangeEnd = 50;
		Range initializationRange = new Range(InitializationRangeStart, InitializationRangeEnd);
		Cuboid initializationCuboid = new Cuboid(initializationRange, initializationRange, initializationRange);
		if (!rebooted)
		{
			_core.Reboot(_steps);
			rebooted = true;
		}
		long result = _core.CountOnCubesInRegion(initializationCuboid);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		if (!rebooted)
		{
			_core.Reboot(_steps);
			rebooted = true;
		}
		long result = _core.CountOnCubes();
		return result.ToString();
	}
}
