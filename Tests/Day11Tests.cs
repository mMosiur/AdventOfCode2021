using System.IO;
using AdventOfCode.Year2021.Day11;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "11")]
public class Day11Tests
{
	const string BaseDirectory = "Inputs/Day11";

	[Theory]
	[InlineData("exampleInput.txt", "1656")]
	[InlineData("myInput.txt", "1640")]
	public void TestDay11Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day11Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "195")]
	[InlineData("myInput.txt", "312")]
	public void TestDay11Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day11Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
