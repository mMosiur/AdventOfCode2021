using AdventOfCode.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day03;

public class Day03Solver : DaySolver
{
	private readonly List<int> _diagnosticReport;
	private readonly DiagnosticReportAnalyzer _analyzer;

	public Day03Solver(string inputFilePath) : base(inputFilePath)
	{
		_diagnosticReport = InputLines.Select(s => Convert.ToInt32(s, 2)).ToList();
		_analyzer = new DiagnosticReportAnalyzer(_diagnosticReport);
	}

	public override string SolvePart1() => _analyzer.PowerConsumption.ToString();

	public override string SolvePart2() => _analyzer.LifeSupportRating.ToString();
}
