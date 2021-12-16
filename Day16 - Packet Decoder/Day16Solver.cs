using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day16;

public class Day16Solver : DaySolver
{
	private readonly PacketReader _packetReader;
	private readonly Lazy<Packet> _outermostPacket;

	public Day16Solver(string inputFilePath) : base(inputFilePath)
	{
		_packetReader = new PacketReader();
		_outermostPacket = new Lazy<Packet>(() => _packetReader.ReadFrom(string.Concat(InputLines)));
	}

	private int SumOfVersions(Packet packet)
	{
		int result = packet.Version;
		if (packet is OperatorPacket operatorPacket)
		{
			result += operatorPacket.Subpackets.Sum(SumOfVersions);
		}
		return result;
	}

	public override string SolvePart1()
	{
		int sumOfVersions = SumOfVersions(_outermostPacket.Value);
		return sumOfVersions.ToString();
	}

	public override string SolvePart2()
	{
		long outermostValue = _outermostPacket.Value.Value;
		return outermostValue.ToString();
	}
}
