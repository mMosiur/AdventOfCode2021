using System.IO;

using AdventOfCode.Year2021.Day24;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "24")]
public class Day24Tests
{
	const string BaseDirectory = "Inputs/Day24";

	[Theory]
	[InlineData("myInput.txt", "39924989499969")]
	public void TestDay24Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day24Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("myInput.txt", "16811412161117")]
	public void TestDay24Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day24Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
