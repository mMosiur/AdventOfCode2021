using System.IO;
using AdventOfCode.Year2021.Day04;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "04")]
[Trait("Day", "4")]
public class Day04Tests
{
	const string BaseDirectory = "Inputs/Day04";

	[Theory]
	[InlineData("exampleInput.txt", "4512")]
	[InlineData("myInput.txt", "82440")]
	public void TestDay04Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day04Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("exampleInput.txt", "1924")]
	[InlineData("myInput.txt", "20774")]
	public void TestDay04Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day04Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}
}
