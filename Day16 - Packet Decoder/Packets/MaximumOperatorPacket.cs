using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day16;

public class MaximumOperatorPacket : OperatorPacket
{
	public MaximumOperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId, subpackets)
	{
		if (subpackets.Count == 0)
		{
			throw new ArgumentException("Maximum operator packet must have at least one subpacket.");
		}
	}

	public override long Value => Subpackets.Max(p => p.Value);
}
