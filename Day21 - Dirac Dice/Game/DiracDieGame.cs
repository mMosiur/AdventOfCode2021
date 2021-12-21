using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day21;

public partial class Game
{
	private class DiracDieGame
	{
		private readonly GameState _startingGameState;
		private readonly IReadOnlyDictionary<int, int> _quantumDieStates;

		private Dictionary<GameState, (long Player1WinCount, long Player2WinCount)> _player1TurnCache = new();
		private Dictionary<GameState, (long Player1WinCount, long Player2WinCount)> _player2TurnCache = new();

		public int ScoreToWin { get; }

		public DiracDieGame(Player player1, Player player2, int scoreToWin, IReadOnlyDictionary<int, int> quantumDieStates)
		{
			_startingGameState = new GameState(player1.Position, player1.Score, player2.Position, player2.Score);
			ScoreToWin = scoreToWin;
			_quantumDieStates = quantumDieStates;
		}

		private (long Player1WinCount, long Player2WinCount) PlayPlayer1Turn(GameState gameState)
		{
			if (gameState.Player2Score >= ScoreToWin) return (0, 1);
			if (_player1TurnCache.TryGetValue(gameState, out var result))
			{
				return result;
			}
			long player1WinCount = 0;
			long player2WinCount = 0;
			foreach ((int rollSum, int occurrences) in _quantumDieStates)
			{
				int newPlayer1Position = (gameState.Player1Position + rollSum - 1) % 10 + 1;
				int newPlayer1Score = gameState.Player1Score + newPlayer1Position;
				GameState newGameState = gameState with { Player1Position = newPlayer1Position, Player1Score = newPlayer1Score };
				var wins = PlayPlayer2Turn(newGameState);
				player1WinCount += wins.Player1WinCount * occurrences;
				player2WinCount += wins.Player2WinCount * occurrences;
			}
			_player1TurnCache[gameState] = (player1WinCount, player2WinCount);
			return (player1WinCount, player2WinCount);
		}

		private (long Player1WinCount, long Player2WinCount) PlayPlayer2Turn(GameState gameState)
		{
			if (gameState.Player1Score >= ScoreToWin) return (1, 0);
			if (_player2TurnCache.TryGetValue(gameState, out var result))
			{
				return result;
			}
			long player1WinCount = 0;
			long player2WinCount = 0;
			foreach ((int rollSum, int occurrences) in _quantumDieStates)
			{
				int newPlayer2Position = (gameState.Player2Position + rollSum - 1) % 10 + 1;
				int newPlayer2Score = gameState.Player2Score + newPlayer2Position;
				GameState newGameState = gameState with { Player2Position = newPlayer2Position, Player2Score = newPlayer2Score };
				var wins = PlayPlayer1Turn(newGameState);
				player1WinCount += wins.Player1WinCount * occurrences;
				player2WinCount += wins.Player2WinCount * occurrences;
			}
			_player2TurnCache.Add(gameState, (player1WinCount, player2WinCount));
			return (player1WinCount, player2WinCount);
		}

		public (long Player1WinCount, long Player2WinCount) Play()
		{
			return PlayPlayer1Turn(_startingGameState);
		}
	}
}
