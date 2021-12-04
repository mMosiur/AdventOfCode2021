using System;
using System.Collections.Generic;
using System.Linq;

public class BingoBoard
{
	private readonly int[,] _boardNumbers;
	private readonly bool[,] _marked;
	private int? _lastNumber;

	public int LastNumber
	{
		get => _lastNumber ?? throw new InvalidOperationException("No number has been called yet.");
		private set => _lastNumber = value;
	}

	public int RowCount => _boardNumbers.GetLength(0);

	public int ColumnCount => _boardNumbers.GetLength(1);

	public IEnumerable<int> UnmarkedNumbers
	{
		get
		{
			for (int row = 0; row < RowCount; row++)
			{
				for (int column = 0; column < ColumnCount; column++)
				{
					if (!_marked[row, column])
						yield return _boardNumbers[row, column];
				}
			}
		}
	}

	public int Score => UnmarkedNumbers.Sum() * LastNumber;

	public BingoBoard(int[,] boardNumbers)
	{
		_boardNumbers = boardNumbers;
		_marked = new bool[RowCount, ColumnCount];
	}

	public void Reset()
	{
		if (!_lastNumber.HasValue)
		{
			return;
		}
		for (int row = 0; row < RowCount; row++)
		{
			for (int column = 0; column < ColumnCount; column++)
			{
				_marked[row, column] = false;
			}
		}
		_lastNumber = null;
	}

	public bool MarkNumber(int number)
	{
		LastNumber = number;
		bool win = false;
		for (int row = 0; row < RowCount; row++)
		{
			for (int column = 0; column < ColumnCount; column++)
			{
				if (_boardNumbers[row, column] != number)
					continue;
				if (_marked[row, column])
					continue;
				_marked[row, column] = true;
				win = win || CheckRow(row) || CheckColumn(column);
			}
		}
		return win;
	}

	private bool CheckRow(int row)
	{
		for (int column = 0; column < ColumnCount; column++)
		{
			if (!_marked[row, column]) return false;
		}
		return true;
	}

	private bool CheckColumn(int column)
	{
		for (int row = 0; row < RowCount; row++)
		{
			if (!_marked[row, column]) return false;
		}
		return true;
	}
}
