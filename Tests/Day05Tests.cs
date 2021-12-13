using System.IO;
using AdventOfCode.Year2021.Day05;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "05")]
[Trait("Day", "5")]
public class Day05Tests
{
	const string BaseDirectory = "Inputs/Day05";

	[Theory]
	[InlineData("exampleInput.txt", "5")]
	[InlineData("myInput.txt", "5197")]
	public void TestDay05Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day05Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "12")]
	[InlineData("myInput.txt", "18605")]
	public void TestDay05Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day05Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
