using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day20;

public class Image : ICloneable, IEnumerable<bool>
{
	public bool[,] Pixels { get; private set; }

	public bool PixelsOutsideScope { get; set; }

	public Image(bool[,] pixels, bool pixelOutsideScope = false)
	{
		Pixels = (bool[,])pixels.Clone();
		PixelsOutsideScope = pixelOutsideScope;
	}

	public Image(char[,] pixels, char onChar, char offChar, bool pixelOutsideScope = false)
	{
		Pixels = new bool[pixels.GetLength(0), pixels.GetLength(1)];
		for (int row = 0; row < Pixels.GetLength(0); row++)
		{
			for (int col = 0; col < Pixels.GetLength(1); col++)
			{
				char pixel = pixels[row, col];
				if (pixel == onChar)
				{
					Pixels[row, col] = true;
				}
				else if (pixel == offChar)
				{
					Pixels[row, col] = false;
				}
				else throw new InvalidOperationException($"Invalid pixel value: '{pixel}'");
			}
		}
		PixelsOutsideScope = pixelOutsideScope;
	}

	public IEnumerable<bool> GetPixelsAround(int row, int col)
	{
		if (row < 0 || row >= Pixels.GetLength(0)) throw new ArgumentOutOfRangeException(nameof(row));
		if (col < 0 || col >= Pixels.GetLength(1)) throw new ArgumentOutOfRangeException(nameof(col));
		for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
		{
			for (int colOffset = -1; colOffset <= 1; colOffset++)
			{
				yield return Pixels.GetValueOrDefault(row + rowOffset, col + colOffset, PixelsOutsideScope);
			}
		}
	}

	public bool HasEmptyBorder()
	{
		int i = 0;
		int j = 0;
		while (i + 1 < Pixels.GetLength(0))
		{
			i++;
			if (Pixels[i, j] != PixelsOutsideScope) return false;
		}
		while (j + 1 < Pixels.GetLength(1))
		{
			j++;
			if (Pixels[i, j] != PixelsOutsideScope) return false;
		}
		while (i - 1 >= 0)
		{
			i--;
			if (Pixels[i, j] != PixelsOutsideScope) return false;
		}
		while (j - 1 >= 0)
		{
			j--;
			if (Pixels[i, j] != PixelsOutsideScope) return false;
		}
		return true;
	}

	public void ZoomOut()
	{
		bool[,] newPixels = new bool[Pixels.GetLength(0) + 2, Pixels.GetLength(1) + 2];
		for (int row = 0; row < newPixels.GetLength(0); row++)
		{
			for (int col = 0; col < newPixels.GetLength(1); col++)
			{
				newPixels[row, col] = Pixels.GetValueOrDefault(row - 1, col - 1, PixelsOutsideScope);
			}
		}
		Pixels = newPixels;
	}

	public object Clone() => new Image(Pixels, PixelsOutsideScope);


	public IEnumerator<bool> GetEnumerator() => Pixels.Cast<bool>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
