using System.IO;
using AdventOfCode.Year2021.Day01;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "01")]
[Trait("Day", "1")]
public class Day01Tests
{
	const string BaseDirectory = "Inputs/Day01";

	[Theory]
	[InlineData("exampleInput.txt", "7")]
	[InlineData("myInput.txt", "1400")]
	public void TestDay01Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day01Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("exampleInput.txt", "5")]
	[InlineData("myInput.txt", "1429")]
	public void TestDay01Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day01Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}
}
