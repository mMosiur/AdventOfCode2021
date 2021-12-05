using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day05;

public class Day05Solver : DaySolver
{
	private readonly VentAnalyzer _ventAnalyzer;

	public Day05Solver(string inputFilePath) : base(inputFilePath)
	{
		List<VentLine> ventLines = InputLines.Select(VentLine.Parse).ToList();
		_ventAnalyzer = new VentAnalyzer(ventLines);
	}

	public override string SolvePart1()
	{
		int result = _ventAnalyzer.OverlapingPointsOfNonDiagonalLines().Count();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = _ventAnalyzer.OverlapingPoints().Count();
		return result.ToString();
	}
}
