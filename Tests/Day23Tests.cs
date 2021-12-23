using System.IO;

using AdventOfCode.Year2021.Day23;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "23")]
public class Day23Tests
{
	const string BaseDirectory = "Inputs/Day23";

	[Theory]
	[InlineData("exampleInput.txt", "12521")]
	[InlineData("myInput.txt", "13066")]
	public void TestDay23Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day23Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "44169")]
	[InlineData("myInput.txt", "47328")]
	public void TestDay23Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day23Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
