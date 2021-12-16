using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day16;

public class SumOperatorPacket : OperatorPacket
{
	public SumOperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId, subpackets)
	{
		if (subpackets.Count == 0)
		{
			throw new InvalidOperationException("Sum operator packet must have at least one subpacket.");
		}
	}

	public override long Value => Subpackets.Sum(p => p.Value);
}
