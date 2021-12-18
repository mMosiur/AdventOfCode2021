using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Year2021.Day17;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "17")]
public class Day17Tests
{
	const string BaseDirectory = "Inputs/Day17";

	[Theory]
	[InlineData("exampleInput.txt", "45")]
	[InlineData("myInput.txt", "23005")]
	public void TestDay17Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day17Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "112")]
	[InlineData("myInput.txt", "2040")]
	public void TestDay17Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day17Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("target area: x=20..30, y=-10..-5", 7, 2)]
	[InlineData("target area: x=20..30, y=-10..-5", 6, 3)]
	[InlineData("target area: x=20..30, y=-10..-5", 9, 0)]
	public void TestInitialVelocitiesThatHitTheTarget(string targetString, int targetX, int targetY)
	{
		Target target = Target.Parse(targetString);
		TrajectoryCalculator calculator = new();
		Velocity velocity = new Velocity(targetX, targetY);
		bool hits = calculator.DoesHitTarget(velocity, target);
		Assert.True(hits);
	}

	[Theory]
	[InlineData("target area: x=20..30, y=-10..-5", 17, -4)]
	public void TestInitialVelocitiesThatDontHitTheTarget(string targetString, int targetX, int targetY)
	{
		Target target = Target.Parse(targetString);
		TrajectoryCalculator calculator = new();
		Velocity velocity = new Velocity(targetX, targetY);
		bool hits = calculator.DoesHitTarget(velocity, target);
		Assert.False(hits);
	}

	[Fact]
	public void TestGivenInitialVelocitiesThatHitTarget()
	{
		const string targetString = "target area: x=20..30, y=-10..-5";
		ISet<(int, int)> expectedSet = new HashSet<(int, int)>
		{
			(23, -10), (25, -9), (27, -5), (29, -6), (22, -6), (21, -7), (9, 0), (27, -7), (24, -5),
			(25, -7), (26, -6), (25, -5), (6, 8), (11, -2), (20, -5), (29, -10), (6, 3), (28, -7),
			(8, 0), (30, -6), (29, -8), (20, -10), (6, 7), (6, 4), (6, 1), (14, -4), (21, -6),
			(26, -10), (7, -1), (7, 7), (8, -1), (21, -9), (6, 2), (20, -7), (30, -10), (14, -3),
			(20, -8), (13, -2), (7, 3), (28, -8), (29, -9), (15, -3), (22, -5), (26, -8), (25, -8),
			(25, -6), (15, -4), (9, -2), (15, -2), (12, -2), (28, -9), (12, -3), (24, -6), (23, -7),
			(25, -10), (7, 8), (11, -3), (26, -7), (7, 1), (23, -9), (6, 0), (22, -10), (27, -6),
			(8, 1), (22, -8), (13, -4), (7, 6), (28, -6), (11, -4), (12, -4), (26, -9), (7, 4),
			(24, -10), (23, -8), (30, -8), (7, 0), (9, -1), (10, -1), (26, -5), (22, -9), (6, 5),
			(7, 5), (23, -6), (28, -10), (10, -2), (11, -1), (20, -9), (14, -2), (29, -7), (13, -3),
			(23, -5), (24, -8), (27, -9), (30, -7), (28, -5), (21, -10), (7, 9), (6, 6), (21, -5),
			(27, -10), (7, 2), (30, -9), (21, -8), (22, -7), (24, -9), (20, -6), (6, 9), (29, -5),
			(8, -2), (27, -8), (30, -5), (24, -7)
		};
		Target target = Target.Parse(targetString);
		TrajectoryCalculator calculator = new();
		bool equal = expectedSet.SetEquals(calculator.InitialVelocitiesThatHitTarget(target).Select(v => (v.X, v.Y)));
		Assert.True(equal);
	}
}
