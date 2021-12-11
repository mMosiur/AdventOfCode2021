using System.IO;
using AdventOfCode.Year2021.Day08;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "08")]
[Trait("Day", "8")]
public class Day08Tests
{
	const string BaseDirectory = "Inputs/Day08";

	[Theory]
	[InlineData("exampleInput.txt", "26")]
	[InlineData("myInput.txt", "342")]
	public void TestDay08Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day08Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("smallExampleInput.txt", "5353")]
	[InlineData("exampleInput.txt", "61229")]
	[InlineData("myInput.txt", "1068933")]
	public void TestDay08Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day08Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}
}
