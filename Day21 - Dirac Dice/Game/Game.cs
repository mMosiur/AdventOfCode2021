using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day21;

public partial class Game
{
	public Player Player1 { get; private set; }
	public Player Player2 { get; private set; }
	public int ScoreToWin { get; }

	public Game(Player player1, Player player2, int scoreToWin)
	{
		Player1 = player1;
		Player2 = player2;
		ScoreToWin = scoreToWin;
	}

	public (Player Winner, Player Loser) Play(DeterministicDie die)
	{
		while (true)
		{
			int rollSum1 = die.Roll() + die.Roll() + die.Roll();
			Player1.Position = (Player1.Position + rollSum1 - 1) % 10 + 1;
			Player1.Score += Player1.Position;
			if (Player1.Score >= ScoreToWin) return (Player1, Player2);

			int rollSum2 = die.Roll() + die.Roll() + die.Roll();
			Player2.Position = (Player2.Position + rollSum2 - 1) % 10 + 1;
			Player2.Score += Player2.Position;
			if (Player2.Score >= ScoreToWin) return (Player2, Player1);
		}
	}

	public (long Player1WinCount, long Player2WinCount) Play(DiracDie die)
	{
		Dictionary<int, int> quantumDieStates = new();
		foreach (int roll1 in die.Roll())
		{
			foreach (int roll2 in die.Roll())
			{
				foreach (int roll3 in die.Roll())
				{
					int rollSum = roll1 + roll2 + roll3;
					quantumDieStates[rollSum] = quantumDieStates.GetValueOrDefault(rollSum, 0) + 1;
				}
			}
		}
		DiracDieGame diracDieGame = new DiracDieGame(Player1, Player2, ScoreToWin, quantumDieStates);
		return diracDieGame.Play();
	}
}
