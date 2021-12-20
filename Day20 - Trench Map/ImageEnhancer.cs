using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day20;

public class ImageEnhancer
{
	private IReadOnlyList<bool> _imageEnhancementAlgorithm;

	public ImageEnhancer(IEnumerable<char> imageEnhancementAlgorithm, char onChar, char offChar)
	{
		int count = imageEnhancementAlgorithm.Count();
		if (count != 0b1000000000)
			throw new ArgumentException("ImageEnhancementAlgorithm must be exactly 512 characters long (9 bits of data)", nameof(imageEnhancementAlgorithm));
		List<bool> algorithm = new List<bool>(count);
		foreach (char c in imageEnhancementAlgorithm)
		{
			if (c == onChar)
			{
				algorithm.Add(true);
			}
			else if (c == offChar)
			{
				algorithm.Add(false);
			}
			else throw new InvalidOperationException($"Invalid pixel value: '{c}'");
		}
		_imageEnhancementAlgorithm = algorithm;
	}

	private static int ToNumber(IEnumerable<bool> bits)
	{
		int result = 0;
		foreach (int bit in bits.Select(b => b ? 1 : 0))
		{
			result = (result << 1) | bit;
		}
		return result;
	}

	public Image Enhance(Image image)
	{
		if (!image.HasEmptyBorder())
		{
			image.ZoomOut();
		}
		Image enhanced = (Image)image.Clone();
		for (int row = 0; row < image.Pixels.GetLength(0); row++)
		{
			for (int col = 0; col < image.Pixels.GetLength(1); col++)
			{
				int number = ToNumber(image.GetPixelsAround(row, col));
				bool newPixel = _imageEnhancementAlgorithm[number];
				enhanced.Pixels[row, col] = newPixel;
			}
		}
		enhanced.PixelsOutsideScope = _imageEnhancementAlgorithm[image.PixelsOutsideScope ? ^1 : 0];
		return enhanced;
	}
}
