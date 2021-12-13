using System.IO;
using AdventOfCode.Year2021.Day07;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "07")]
[Trait("Day", "7")]
public class Day07Tests
{
	const string BaseDirectory = "Inputs/Day07";

	[Theory]
	[InlineData("exampleInput.txt", "37")]
	[InlineData("myInput.txt", "356958")]
	public void TestDay07Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day07Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "168")]
	[InlineData("myInput.txt", "105461913")]
	public void TestDay07Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day07Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
