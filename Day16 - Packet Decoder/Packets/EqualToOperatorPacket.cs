using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day16;

public class EqualToOperatorPacket : OperatorPacket
{
	public EqualToOperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId, subpackets)
	{
		if (Subpackets.Count != 2)
		{
			throw new ArgumentException($"Expected 2 subpackets, got {Subpackets.Count}.", nameof(subpackets));
		}
	}

	public override long Value
	{
		get
		{
			var it = Subpackets.GetEnumerator();
			it.MoveNext();
			long first = it.Current.Value;
			it.MoveNext();
			long second = it.Current.Value;
			return first == second ? 1L : 0L;
		}
	}
}
