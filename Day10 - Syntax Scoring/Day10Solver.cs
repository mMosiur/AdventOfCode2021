using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day10;

public class Day10Solver : DaySolver
{
	private readonly SyntaxChecker _syntaxChecker;
	private readonly Lazy<IReadOnlyList<ISyntaxResult>> _syntaxResults;

	public Day10Solver(string inputFilePath) : base(inputFilePath)
	{
		_syntaxChecker = new SyntaxChecker();
		_syntaxResults = new Lazy<IReadOnlyList<ISyntaxResult>>(GetSyntaxResults);
	}

	public IReadOnlyList<ISyntaxResult> SyntaxResults => _syntaxResults.Value;

	private IReadOnlyList<ISyntaxResult> GetSyntaxResults()
	{
		return InputLines.Select(_syntaxChecker.Check).ToList();
	}

	public override string SolvePart1()
	{
		int result = SyntaxResults
			.Where(result => result.Type == SyntaxResultType.Corrupted)
			.Cast<CorruptedSyntaxResult>()
			.Select(result => result.Score)
			.Sum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		IEnumerable<long> results = SyntaxResults
			.Where(r => r.Type == SyntaxResultType.Incomplete)
			.Cast<IncompleteSyntaxResult>()
			.Select(r => r.Score);
		double median = Statistics.Median(results.Select(r => (double)r));
		long middleScore = Convert.ToInt64(median);
		return middleScore.ToString();
	}
}
