using System;
using System.IO;
using System.Text.RegularExpressions;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day21;

public class Day21Solver : DaySolver
{
	private const int MinSpaceNumber = 1;
	private const int MaxSpaceNumber = 10;

	private readonly int _player1StartingSpace;
	private readonly int _player2StartingSpace;

	public Day21Solver(string inputFilePath) : base(inputFilePath)
	{
		Regex regex = new Regex(@"^Player (\d+) starting position: (\d+)$");
		var it = InputLines.GetEnumerator();
		if (!it.MoveNext()) throw new InvalidDataException("No starting space found in file.");
		Match match = regex.Match(it.Current);
		if (!match.Success) throw new InvalidDataException("Invalid format of a starting space line.");
		int playerNumber1 = int.Parse(match.Groups[1].ValueSpan);
		int startingSpace1 = int.Parse(match.Groups[2].ValueSpan);
		if (!it.MoveNext()) throw new InvalidDataException("No second starting space found in file.");
		match = regex.Match(it.Current);
		if (!match.Success) throw new InvalidDataException("Invalid format of a second starting space line.");
		int playerNumber2 = int.Parse(match.Groups[1].ValueSpan);
		int startingSpace2 = int.Parse(match.Groups[2].ValueSpan);
		if (playerNumber2 + 1 == playerNumber1)
		{
			int tmp = playerNumber1;
			playerNumber1 = playerNumber2;
			playerNumber2 = tmp;
			tmp = startingSpace1;
			startingSpace1 = startingSpace2;
			startingSpace2 = tmp;
		}
		if ((playerNumber1 != 0 && playerNumber1 != 1) || (playerNumber2 != playerNumber1 + 1))
			throw new InvalidDataException("Invalid player numbers. Player must be 0 and 1, or 1 and 2");
		if (startingSpace1 < MinSpaceNumber || startingSpace1 > MaxSpaceNumber || startingSpace2 < MinSpaceNumber || startingSpace2 > MaxSpaceNumber)
			throw new InvalidDataException($"Starting space must be between {MinSpaceNumber} and {MaxSpaceNumber}");
		_player1StartingSpace = startingSpace1;
		_player2StartingSpace = startingSpace2;
	}

	public override string SolvePart1()
	{
		const int ScoreToWin = 1000;
		Player player1 = new Player(_player1StartingSpace);
		Player player2 = new Player(_player2StartingSpace);
		Game game = new Game(player1, player2, ScoreToWin);
		const int DieSides = 100;
		var die = new DeterministicDie(DieSides);
		(_, Player loser) = game.Play(die);
		int result = loser.Score * die.RollCount;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int ScoreToWin = 21;
		Player player1 = new Player(_player1StartingSpace);
		Player player2 = new Player(_player2StartingSpace);
		Game game = new Game(player1, player2, ScoreToWin);
		const int DieSides = 3;
		var die = new DiracDie(DieSides);
		(long player1WinCount, long player2WinCount) = game.Play(die);
		long result = Math.Max(player1WinCount, player2WinCount);
		return result.ToString();
	}
}
