using System.IO;

using AdventOfCode.Year2021.Day22;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "22")]
public class Day22Tests
{
	const string BaseDirectory = "Inputs/Day22";

	[Theory]
	[InlineData("exampleInput1.txt", "39")]
	[InlineData("exampleInput2.txt", "590784")]
	[InlineData("exampleInput3.txt", "474140")]
	[InlineData("myInput.txt", "642125")]
	public void TestDay22Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day22Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput3.txt", "2758514936282235")]
	[InlineData("myInput.txt", "1235164413198198")]
	public void TestDay22Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day22Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
