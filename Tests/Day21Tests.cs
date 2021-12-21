using System.IO;

using AdventOfCode.Year2021.Day21;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "21")]
public class Day21Tests
{
	const string BaseDirectory = "Inputs/Day21";

	[Theory]
	[InlineData("exampleInput.txt", "739785")]
	[InlineData("myInput.txt", "998088")]
	public void TestDay21Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day21Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "444356092776315")]
	[InlineData("myInput.txt", "306621346123766")]
	public void TestDay21Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day21Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
