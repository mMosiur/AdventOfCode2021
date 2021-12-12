using System.IO;
using AdventOfCode.Year2021.Day12;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "12")]
public class Day12Tests
{
	const string BaseDirectory = "Inputs/Day12";

	[Theory]
	[InlineData("exampleInput1.txt", "10")]
	[InlineData("exampleInput2.txt", "19")]
	[InlineData("exampleInput3.txt", "226")]
	[InlineData("myInput.txt", "3708")]
	public void TestDay12Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day12Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("exampleInput1.txt", "36")]
	[InlineData("exampleInput2.txt", "103")]
	[InlineData("exampleInput3.txt", "3509")]
	[InlineData("myInput.txt", "93858")]
	public void TestDay12Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day12Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}
}
