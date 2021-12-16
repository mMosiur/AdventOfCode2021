using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day16;

public abstract class OperatorPacket : Packet
{
	public IReadOnlyCollection<Packet> Subpackets { get; }

	public OperatorPacket(int version, int typeId, IReadOnlyCollection<Packet> subpackets) : base(version, typeId)
	{
		Subpackets = subpackets;
	}
}
