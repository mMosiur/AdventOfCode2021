namespace AdventOfCode.Year2021.Day16;

public class LiteralValuePacket : Packet
{
	public override long Value { get; }

	public LiteralValuePacket(int version, int typeId, long value) : base(version, typeId)
	{
		Value = value;
	}
}
