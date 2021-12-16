using System;

namespace AdventOfCode.Year2021.Day16;

public class BitArrayScanner
{
	private readonly BitArray _bitArray;

	public int Position { get; private set; }
	public bool Finished => Position >= _bitArray.Count;

	public BitArrayScanner(BitArray bitArray)
	{
		_bitArray = bitArray;
		Position = 0;
	}

	public int ReadNextBits(int bitCount)
	{
		if (Position + bitCount > _bitArray.Count)
		{
			throw new ArgumentOutOfRangeException(nameof(bitCount));
		}
		int result = 0;
		for (int i = 0; i < bitCount; i++)
		{
			result <<= 1;
			result |= _bitArray[Position++] ? 1 : 0;
		}
		return result;
	}

	public bool ReadNextBit()
	{
		if (Finished)
		{
			throw new InvalidOperationException();
		}
		return _bitArray[Position++];
	}
}
