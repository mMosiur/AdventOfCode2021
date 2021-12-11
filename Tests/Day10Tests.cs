using System.IO;
using AdventOfCode.Year2021.Day10;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Day", "10")]
public class Day10Tests
{
	const string BaseDirectory = "Inputs/Day10";

	[Theory]
	[InlineData("exampleInput.txt", "26397")]
	[InlineData("myInput.txt", "265527")]
	public void TestPart1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day10Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("exampleInput.txt", "288957")]
	[InlineData("myInput.txt", "3969823589")]
	public void TestPart2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day10Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(result, expectedResult);
	}

	[Theory]
	[InlineData("([])")]
	[InlineData("{()()()}")]
	[InlineData("<([{}])>")]
	[InlineData("[<>({}){}[([])<>]]")]
	[InlineData("(((((((((())))))))))")]
	public void TestValidChunks(string chunk)
	{
		SyntaxChecker checker = new SyntaxChecker();
		ISyntaxResult result = checker.Check(chunk);
		Assert.Equal(SyntaxResultType.Correct, result.Type);
		Assert.IsType<CorrectSyntaxResult>(result);
	}

	[Theory]
	[InlineData("(]")]
	[InlineData("{()()()>")]
	[InlineData("(((()))}")]
	[InlineData("<([]){()}[{}])")]
	public void TestCorruptedChunks(string chunk)
	{
		SyntaxChecker checker = new SyntaxChecker();
		ISyntaxResult result = checker.Check(chunk);
		Assert.Equal(SyntaxResultType.Corrupted, result.Type);
		Assert.IsType<CorruptedSyntaxResult>(result);
	}


	[Theory]
	[InlineData("{([(<{}[<>[]}>{[]{[(<()>", '}')]
	[InlineData("[[<[([]))<([[{}[[()]]]", ')')]
	[InlineData("[{[{({}]{}}([{[{{{}}([]", ']')]
	[InlineData("[<(<(<(<{}))><([]([]()", ')')]
	[InlineData("<{([([[(<>()){}]>(<<{{", '>')]
	public void TestCorruptedChunksWithGivenError(string chunk, char expectedError)
	{
		SyntaxChecker checker = new SyntaxChecker();
		ISyntaxResult result = checker.Check(chunk);
		Assert.Equal(SyntaxResultType.Corrupted, result.Type);
		Assert.IsType<CorruptedSyntaxResult>(result);
		CorruptedSyntaxResult corruptedResult = (CorruptedSyntaxResult)result;
		Assert.Equal(expectedError, corruptedResult.FirstIllegalCharacter);
	}

	[Theory]
	[InlineData("[({(<(())[]>[[{[]{<()<>>", "}}]])})]", 288957)]
	[InlineData("[(()[<>])]({[<{<<[]>>(", ")}>]})", 5566)]
	[InlineData("(((({<>}<{<{<>}{[]{[]{}", "}}>}>))))", 1480781)]
	[InlineData("{<[[]]>}<{[{[{[]{()[[[]", "]]}}]}]}>", 995444)]
	[InlineData("<{([{{}}[<[[[<>{}]]]>[]]", "])}>", 294)]
	public void TestIncompleteChunksWithGivenCompletionStringAndScore(string chunk, string expectedCompletionString, int expectedScore)
	{
		SyntaxChecker checker = new SyntaxChecker();
		ISyntaxResult result = checker.Check(chunk);
		Assert.Equal(SyntaxResultType.Incomplete, result.Type);
		Assert.IsType<IncompleteSyntaxResult>(result);
		IncompleteSyntaxResult incomplete = (IncompleteSyntaxResult)result;
		Assert.Equal(expectedCompletionString, incomplete.CompletionString);
		Assert.Equal(expectedScore, incomplete.Score);
	}
}
