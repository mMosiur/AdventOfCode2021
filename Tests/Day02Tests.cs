using System.IO;
using AdventOfCode.Year2021.Day02;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "02")]
[Trait("Day", "2")]
public class Day02Tests
{
	const string BaseDirectory = "Inputs/Day02";

	[Theory]
	[InlineData("exampleInput.txt", "150")]
	[InlineData("myInput.txt", "2322630")]
	public void TestDay02Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day02Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "900")]
	[InlineData("myInput.txt", "2105273490")]
	public void TestDay02Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day02Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
