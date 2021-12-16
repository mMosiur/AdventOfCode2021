using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day16;

public abstract class Packet
{
	public int Version { get; }
	public int TypeID { get; }

	public abstract long Value { get; }

	public Packet(int version, int typeId)
	{
		Version = version;
		TypeID = typeId;
	}
}
