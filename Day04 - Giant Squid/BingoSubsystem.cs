using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2021.Day04;

public class BingoSubsystem
{
	private readonly IList<int> _numbers;
	private readonly ICollection<BingoBoard> _boards;

	public BingoSubsystem(IList<int> numbers, ICollection<BingoBoard> boards)
	{
		_numbers = numbers;
		_boards = boards;
	}

	public BingoBoard GetFirstWinner()
	{
		Reset();
		foreach (int number in _numbers)
		{
			foreach (BingoBoard board in _boards)
			{
				bool win = board.MarkNumber(number);
				if (win)
				{
					return board;
				}
			}
		}
		throw new InvalidDataException("No winning boards found with given numbers.");
	}

	public BingoBoard GetLastWinner()
	{
		Reset();
		HashSet<BingoBoard> boardsLeft = new HashSet<BingoBoard>(_boards);
		foreach (int number in _numbers)
		{
			foreach (BingoBoard board in boardsLeft)
			{
				bool win = board.MarkNumber(number);
				if (win)
				{
					boardsLeft.Remove(board);
					if (boardsLeft.Count == 0)
					{
						return board;
					}
				}
			}
		}
		throw new InvalidDataException("Not all boards have won with given numbers.");
	}

	public void Reset()
	{
		foreach (BingoBoard board in _boards)
		{
			board.Reset();
		}
	}
}
