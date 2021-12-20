using System.Collections.Generic;
using System.IO;
using System.Linq;

using AdventOfCode.Year2021.Day18;

using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "18")]
public class Day18Tests
{
	const string BaseDirectory = "Inputs/Day18";

	[Theory]
	[InlineData("exampleInput.txt", "4140")]
	[InlineData("myInput.txt", "2501")]
	public void TestDay18Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day18Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("exampleInput.txt", "3993")]
	[InlineData("myInput.txt", "4935")]
	public void TestDay18Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day18Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]", 1)]
	[InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]", 1)]
	[InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]", 1)]
	[InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]", 2)]
	public void TestPairReduction(string inputPairString, string expectedReducedPairString, int expectedSteps)
	{
		Pair inputPair = Pair.Parse(inputPairString);
		int steps = inputPair.Reduce();
		string reducedPairString = inputPair.ToString();
		Assert.Equal(expectedReducedPairString, reducedPairString);
		Assert.Equal(expectedSteps, steps);
	}

	[Theory]
	[InlineData("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
	public void TestPairAddition(string inputPair1String, string inputPair2String, string expectedPairResultString)
	{
		Pair inputPair1 = Pair.Parse(inputPair1String);
		Pair inputPair2 = Pair.Parse(inputPair2String);
		Pair pairResult = inputPair1 + inputPair2;
		string pairResultString = pairResult.ToString();
		Assert.Equal(expectedPairResultString, pairResultString);
	}

	[Theory]
	[MemberData(nameof(TestListOfPairsAdditionData))]
	public void TestListOfPairsAddition(string[] listOfPairStrings, string expectedPairResultString)
	{
		Pair pairResult = listOfPairStrings.Select(Pair.Parse).Sum();
		string pairResultString = pairResult.ToString();
		Assert.Equal(expectedPairResultString, pairResultString);
	}

	[Theory]
	[InlineData("[9,1]", 29)]
	[InlineData("[1,9]", 21)]
	[InlineData("[[9,1],[1,9]]", 129)]
	[InlineData("[[1,2],[[3,4],5]]", 143)]
	[InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
	[InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
	[InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
	[InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
	[InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
	public void TestPairMagnitude(string inputPairString, int expectedMagnitude)
	{
		Pair inputPair = Pair.Parse(inputPairString);
		int magnitude = inputPair.Magnitude;
		Assert.Equal(expectedMagnitude, magnitude);
	}

	public static IEnumerable<object[]> TestListOfPairsAdditionData
	{
		get
		{
			yield return new object[]{
				new string[]
				{
					"[1,1]",
					"[2,2]",
					"[3,3]",
					"[4,4]"
				},
				"[[[[1,1],[2,2]],[3,3]],[4,4]]"
			};
			yield return new object[]{
				new string[]
				{
					"[1,1]",
					"[2,2]",
					"[3,3]",
					"[4,4]",
					"[5,5]"
				},
				"[[[[3,0],[5,3]],[4,4]],[5,5]]"
			};
			yield return new object[]{
				new string[]
				{
					"[1,1]",
					"[2,2]",
					"[3,3]",
					"[4,4]",
					"[5,5]",
					"[6,6]"
				},
				"[[[[5,0],[7,4]],[5,5]],[6,6]]"
			};
			yield return new object[]{
				new string[]
				{
					"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
					"[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
					"[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
					"[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
					"[7,[5,[[3,8],[1,4]]]]",
					"[[2,[2,2]],[8,[8,1]]]",
					"[2,9]",
					"[1,[[[9,3],9],[[9,0],[0,7]]]]",
					"[[[5,[7,4]],7],1]",
					"[[[[4,2],2],6],[8,7]]"
				},
				"[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]"
			};
			yield return new object[]{
				new string[]
				{
					"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
					"[[[5,[2,8]],4],[5,[[9,9],0]]]",
					"[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
					"[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
					"[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
					"[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
					"[[[[5,4],[7,7]],8],[[8,3],8]]",
					"[[9,3],[[9,9],[6,[4,9]]]]",
					"[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
					"[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
				},
				"[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]"
			};
		}
	}
}
