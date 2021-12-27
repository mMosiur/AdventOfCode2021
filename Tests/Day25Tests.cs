using System.IO;

using AdventOfCode.Year2021.Day25;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "25")]
public class Day25Tests
{
	const string BaseDirectory = "Inputs/Day25";

	[Theory]
	[InlineData("exampleInput.txt", "58")]
	[InlineData("myInput.txt", "504")]
	public void TestDay25Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day25Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	// No test for part 2, as there is no part 2.
	// Merry Christmas!
}
