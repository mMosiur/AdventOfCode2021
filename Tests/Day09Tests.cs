using System.IO;
using AdventOfCode.Year2021.Day09;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

public class Day09Tests
{
	const string BaseDirectory = "Inputs/Day09";

	[Theory]
	[InlineData("exampleInput.txt", "15")]
	[InlineData("myInput.txt", "480")]
	public void TestDay09Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day09Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("exampleInput.txt", "1134")]
	[InlineData("myInput.txt", "1045660")]
	public void TestDay09Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day09Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}
}
