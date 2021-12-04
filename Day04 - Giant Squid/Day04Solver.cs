using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day04;

public class Day04Solver : DaySolver
{
	private readonly BingoSubsystem _bingoSubsystem;

	public Day04Solver(string inputFilePath) : base(inputFilePath)
	{
		using IEnumerator<string> it = InputLines.GetEnumerator();
		if (!it.MoveNext()) throw new FormatException("Input file is empty.");
		List<int> numbers = it.Current.Split(',').Select(int.Parse).ToList();
		if (!it.MoveNext()) throw new FormatException("Input file had no bingo boards.");
		if (!it.Current.Equals("")) throw new FormatException("Input was in invalid format");
		IList<BingoBoard> boards = new List<BingoBoard>();
		while (it.MoveNext())
		{
			IList<IList<int>> boardNumbers = new List<IList<int>>(5);
			while (!it.Current.Equals(""))
			{
				boardNumbers.Add(
					it.Current
						.Split(' ', StringSplitOptions.RemoveEmptyEntries)
						.Select(int.Parse)
						.ToList()
				);
				if (!it.MoveNext()) break;
			}
			boards.Add(new BingoBoard(boardNumbers.To2dArray()));
		}
		_bingoSubsystem = new BingoSubsystem(numbers, boards);
	}

	public override string SolvePart1()
	{
		return _bingoSubsystem.GetFirstWinner().Score.ToString();
	}

	public override string SolvePart2()
	{
		BingoBoard board = _bingoSubsystem.GetLastWinner();
		return board.Score.ToString();
	}
}
