using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day16;

public class ProductOperatorPacket : OperatorPacket
{
	public ProductOperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId, subpackets)
	{
		if (Subpackets.Count == 0)
		{
			throw new InvalidOperationException("Product operator packet must have at least one subpacket.");
		}
	}

	public override long Value => Subpackets.Aggregate(1L, (acc, p) => acc * p.Value);
}
