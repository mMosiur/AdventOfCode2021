using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day16;

public class MinimumOperatorPacket : OperatorPacket
{
	public MinimumOperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId, subpackets)
	{
		if (Subpackets.Count == 0)
		{
			throw new ArgumentException("Minimum operator packet must have at least one subpacket.");
		}
	}

	public override long Value => Subpackets.Min(p => p.Value);
}
