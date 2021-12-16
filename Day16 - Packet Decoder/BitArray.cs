using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day16;

public class BitArray : IReadOnlyList<bool>
{
	private byte[] _bytes;

	public int Count => _bytes.Length * 8;

	public bool this[int index]
	{
		get
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException();
			}
			int byteIndex = index / 8;
			int bitIndex = index % 8;
			return (_bytes[byteIndex] & 128 >> bitIndex) != 0;
		}
	}

	public BitArray(byte[] bytes)
	{
		_bytes = bytes;
	}

	public IEnumerator<bool> GetEnumerator()
	{
		for (int i = 0; i < Count; i++)
		{
			yield return this[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
