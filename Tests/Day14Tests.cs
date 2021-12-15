using System.IO;
using AdventOfCode.Year2021.Day14;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "14")]
public class Day14Tests
{
	const string BaseDirectory = "Inputs/Day14";

	[Theory]
	[InlineData("exampleInput.txt", "1588")]
	[InlineData("myInput.txt", "3306")]
	public void TestDay14Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day14Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "2188189693529")]
	[InlineData("myInput.txt", "3760312702877")]
	public void TestDay14Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day14Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
