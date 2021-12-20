using System.IO;
using System.Linq;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day20;

public class Day20Solver : DaySolver
{
	private readonly Image _inputImage;
	private readonly ImageEnhancer _imageEnhancer;

	public Day20Solver(string inputFilePath) : base(inputFilePath)
	{
		const char onChar = '#';
		const char offChar = '.';
		SegmentReader reader = new SegmentReader(InputLines);
		var it = reader.GetEnumerator();
		if (!it.MoveNext()) throw new InvalidDataException();
		string imageEnhancementAlgorithmLine = string.Concat(it.Current);
		if (!it.MoveNext()) throw new InvalidDataException();
		string[] inputImageLines = it.Current.ToArray();
		if (it.MoveNext()) throw new InvalidDataException();
		_imageEnhancer = new ImageEnhancer(imageEnhancementAlgorithmLine, onChar, offChar);
		_inputImage = new Image(inputImageLines.ToMatrix(), onChar, offChar);
	}

	public override string SolvePart1()
	{
		Image image = (Image)_inputImage.Clone();
		for (int i = 0; i < 2; i++)
		{
			image = _imageEnhancer.Enhance(image);
		}
		int litPixels = image.Count(pixel => pixel == true);
		return litPixels.ToString();
	}

	public override string SolvePart2()
	{
		Image image = (Image)_inputImage.Clone();
		for (int i = 0; i < 50; i++)
		{
			image = _imageEnhancer.Enhance(image);
		}
		int litPixels = image.Count(pixel => pixel == true);
		return litPixels.ToString();
	}
}
