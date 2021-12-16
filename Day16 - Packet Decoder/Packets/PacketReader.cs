using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day16;

public class PacketReader
{
	public Packet ReadFrom(string hexadecimalString)
	{
		string message = hexadecimalString;
		int size = (int)Math.Ceiling(message.Length / 2D);
		byte[] bytes = new byte[size];
		for (int i = 0; i < message.Length / 2; i++)
		{
			bytes[i] = Convert.ToByte(message.Substring(i * 2, 2), 16);
		}
		if (message.Length / 2 < size)
		{
			bytes[^1] = Convert.ToByte($"{message[^1]}0", 16);
		}
		BitArray bitArray = new(bytes);
		return ReadFrom(bitArray);
	}

	public Packet ReadFrom(BitArray bitArray)
	{
		BitArrayScanner scanner = new(bitArray);
		return ReadFrom(scanner);
	}

	public Packet ReadFrom(BitArrayScanner scanner)
	{
		int version = scanner.ReadNextBits(3);
		int typeId = scanner.ReadNextBits(3);
		if (typeId == 4)
		{
			long literal = 0L;
			bool hasContinuation = true;
			while (hasContinuation)
			{
				hasContinuation = scanner.ReadNextBit();
				literal <<= 4;
				literal += scanner.ReadNextBits(4);
			}
			return new LiteralValuePacket(version, typeId, literal);
		}
		else
		{
			ICollection<Packet> subpackets;
			bool subpacketCountBased = scanner.ReadNextBit();
			if (subpacketCountBased)
			{
				int subpacketCount = scanner.ReadNextBits(11);
				subpackets = new List<Packet>(subpacketCount);
				while (subpackets.Count < subpacketCount)
				{
					Packet subpacket = ReadFrom(scanner);
					subpackets.Add(subpacket);
				}
			}
			else
			{
				int subpacketsLength = scanner.ReadNextBits(15);
				int endPosition = scanner.Position + subpacketsLength;
				subpackets = new List<Packet>();
				while (scanner.Position < endPosition)
				{
					Packet subpacket = ReadFrom(scanner);
					subpackets.Add(subpacket);
				}
			}
			return typeId switch
			{
				0 => new SumOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				1 => new ProductOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				2 => new MinimumOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				3 => new MaximumOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				5 => new GreaterThanOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				6 => new LessThanOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				7 => new EqualToOperatorPacket(version, typeId, (IReadOnlyCollection<Packet>)subpackets),
				_ => throw new InvalidOperationException($"Unexpected ID: {typeId}")
			};
		}
	}
}
