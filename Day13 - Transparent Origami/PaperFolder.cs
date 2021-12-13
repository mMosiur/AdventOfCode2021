using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2021.Day13;

public class PaperFolder : IEnumerable<PaperSpot>
{
	private PaperSpot[,] _paper;

	public PaperFolder(PaperSpot[,] paper)
	{
		_paper = paper;
	}

	public static PaperFolder FromDotsPositions(IEnumerable<(int X, int Y)> positions)
	{
		ICollection<(int X, int Y)> dots = positions.ToList();
		int maxY = dots.Max(dot => dot.Y);
		int maxX = dots.Max(dot => dot.X);
		PaperSpot[,] paper = new PaperSpot[maxY + 1, maxX + 1];
		for (int y = 0; y <= maxY; y++)
		{
			for (int x = 0; x <= maxX; x++)
			{
				paper[y, x] = PaperSpot.Empty;
			}
		}
		foreach (var dot in dots)
		{
			paper[dot.Y, dot.X] = PaperSpot.Dot;
		}
		return new PaperFolder(paper);
	}

	private void FoldLeft(int position)
	{
		if (position < 0 || position >= _paper.GetLength(1) - 1)
		{
			throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
		}
		PaperSpot[,] foldedPaper = new PaperSpot[_paper.GetLength(0), position];
		for (int y = 0; y < foldedPaper.GetLength(0); y++)
		{
			for (int x = 0; x < foldedPaper.GetLength(1); x++)
			{
				foldedPaper[y, x] = _paper[y, x];
			}
		}
		for (int y = 0; y < _paper.GetLength(0); y++)
		{
			for (int x = 1; x < _paper.GetLength(1) - position; x++)
			{
				int oldX = position + x;
				int newX = position - x;
				if (_paper[y, oldX] == PaperSpot.Empty) continue;
				foldedPaper[y, newX] = _paper[y, oldX];
			}
		}
		_paper = foldedPaper;
	}

	private void FoldUp(int position)
	{
		if (position < 0 || position >= _paper.GetLength(0) - 1)
		{
			throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
		}
		PaperSpot[,] foldedPaper = new PaperSpot[position, _paper.GetLength(1)];
		for (int y = 0; y < foldedPaper.GetLength(0); y++)
		{
			for (int x = 0; x < foldedPaper.GetLength(1); x++)
			{
				foldedPaper[y, x] = _paper[y, x];
			}
		}
		for (int y = 1; y < _paper.GetLength(0) - position; y++)
		{
			for (int x = 0; x < _paper.GetLength(1); x++)
			{
				int oldY = position + y;
				int newY = position - y;
				if (_paper[oldY, x] == PaperSpot.Empty) continue;
				foldedPaper[newY, x] = _paper[oldY, x];
			}
		}
		_paper = foldedPaper;
	}

	public void Fold(FoldInstruction instruction)
	{
		switch (instruction.Direction)
		{
			case FoldDirection.Left:
				FoldLeft(instruction.Position);
				break;
			case FoldDirection.Up:
				FoldUp(instruction.Position);
				break;
			default:
				throw new InvalidOperationException();
		}
	}

	public IEnumerator<PaperSpot> GetEnumerator() => _paper.Cast<PaperSpot>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public override string ToString()
	{
		StringBuilder builder = new StringBuilder();
		for (int x = 0; x < _paper.GetLength(1); x++)
		{
			builder.Append((char)_paper[0, x]);
		}
		for (int y = 1; y < _paper.GetLength(0); y++)
		{
			builder.AppendLine();
			for (int x = 0; x < _paper.GetLength(1); x++)
			{
				builder.Append((char)_paper[y, x]);
			}
		}
		return builder.ToString();
	}
}
