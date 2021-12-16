using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Year2021.Day16;
using Xunit;

namespace AdventOfCode.Year2021.Tests;

[Trait("Year", "2021")]
[Trait("Day", "16")]
public class Day16Tests
{
	const string BaseDirectory = "Inputs/Day16";

	[Theory]
	[InlineData("myInput.txt", "843")]
	public void TestDay16Part1(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day16Solver(filepath);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData("myInput.txt", "5390807940351")]
	public void TestDay16Part2(string inputFilename, string expectedResult)
	{
		string filepath = Path.Combine(BaseDirectory, inputFilename);
		var solver = new Day16Solver(filepath);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}

	[Fact]
	public void TestPacketRepresentedByHexadecimalStringD2FE28()
	{
		const string HexadecimalString = "D2FE28";
		const int ExpectedVersion = 6;
		const int ExpectedTypeID = 4;
		const int ExpectedLiteralValue = 2021;
		PacketReader packetReader = new();
		Packet packet = packetReader.ReadFrom(HexadecimalString);
		LiteralValuePacket literalValuePacket = Assert.IsType<LiteralValuePacket>(packet);
		Assert.Equal(ExpectedVersion, literalValuePacket.Version);
		Assert.Equal(ExpectedTypeID, literalValuePacket.TypeID);
		Assert.Equal(ExpectedLiteralValue, literalValuePacket.Value);
	}

	[Fact]
	public void TestPacketRepresentedByHexadecimalString38006F45291200()
	{
		const string HexadecimalString = "38006F45291200";
		const int ExpectedVersion = 1;
		const int ExpectedTypeID = 6;
		const int ExpectedSubpacketCount = 2;
		const int ExpectedSubpacket0TypeID = 4;
		const int ExpectedSubpacket0Value = 10;
		const int ExpectedSubpacket1TypeID = 4;
		const int ExpectedSubpacket1Value = 20;
		PacketReader packetReader = new();
		Packet packet = packetReader.ReadFrom(HexadecimalString);
		OperatorPacket operatorPacket = Assert.IsAssignableFrom<OperatorPacket>(packet);
		Assert.Equal(ExpectedVersion, operatorPacket.Version);
		Assert.Equal(ExpectedTypeID, operatorPacket.TypeID);
		List<Packet> subpackets = operatorPacket.Subpackets.ToList();
		Assert.Equal(ExpectedSubpacketCount, subpackets.Count);
		LiteralValuePacket subpacket0 = Assert.IsType<LiteralValuePacket>(subpackets[0]);
		Assert.Equal(ExpectedSubpacket0TypeID, subpacket0.TypeID);
		Assert.Equal(ExpectedSubpacket0Value, subpacket0.Value);
		LiteralValuePacket subpacket1 = Assert.IsType<LiteralValuePacket>(subpackets[1]);
		Assert.Equal(ExpectedSubpacket1TypeID, subpacket1.TypeID);
		Assert.Equal(ExpectedSubpacket1Value, subpacket1.Value);
	}

	[Fact]
	public void TestPacketRepresentedByHexadecimalStringEE00D40C823060()
	{
		const string HexadecimalString = "EE00D40C823060";
		const int ExpectedVersion = 7;
		const int ExpectedTypeID = 3;
		const int ExpectedSubpacketCount = 3;
		const int ExpectedSubpacket0TypeID = 4;
		const int ExpectedSubpacket0Value = 1;
		const int ExpectedSubpacket1TypeID = 4;
		const int ExpectedSubpacket1Value = 2;
		const int ExpectedSubpacket2TypeID = 4;
		const int ExpectedSubpacket2Value = 3;
		PacketReader packetReader = new();
		Packet packet = packetReader.ReadFrom(HexadecimalString);
		OperatorPacket operatorPacket = Assert.IsAssignableFrom<OperatorPacket>(packet);
		Assert.Equal(ExpectedVersion, operatorPacket.Version);
		Assert.Equal(ExpectedTypeID, operatorPacket.TypeID);
		List<Packet> subpackets = operatorPacket.Subpackets.ToList();
		Assert.Equal(ExpectedSubpacketCount, subpackets.Count);
		LiteralValuePacket subpacket0 = Assert.IsType<LiteralValuePacket>(subpackets[0]);
		Assert.Equal(ExpectedSubpacket0TypeID, subpacket0.TypeID);
		Assert.Equal(ExpectedSubpacket0Value, subpacket0.Value);
		LiteralValuePacket subpacket1 = Assert.IsType<LiteralValuePacket>(subpackets[1]);
		Assert.Equal(ExpectedSubpacket1TypeID, subpacket1.TypeID);
		Assert.Equal(ExpectedSubpacket1Value, subpacket1.Value);
		LiteralValuePacket subpacket2 = Assert.IsType<LiteralValuePacket>(subpackets[2]);
		Assert.Equal(ExpectedSubpacket2TypeID, subpacket2.TypeID);
		Assert.Equal(ExpectedSubpacket2Value, subpacket2.Value);
	}

	/// More tests can be added for the other packet types.
	/*
	8A004A801A8002F478 represents an operator packet (version 4) which contains an operator packet (version 1) which contains an operator packet (version 5) which contains a literal value (version 6); this packet has a version sum of 16.
	620080001611562C8802118E34 represents an operator packet (version 3) which contains two sub-packets; each sub-packet is an operator packet that contains two literal values. This packet has a version sum of 12.
	C0015000016115A2E0802F182340 has the same structure as the previous example, but the outermost packet uses a different length type ID. This packet has a version sum of 23.
	A0016C880162017C3686B18A3D4780 is an operator packet that contains an operator packet that contains an operator packet that contains five literal values; it has a version sum of 31.

	C200B40A82 finds the sum of 1 and 2, resulting in the value 3.
	04005AC33890 finds the product of 6 and 9, resulting in the value 54.
	880086C3E88112 finds the minimum of 7, 8, and 9, resulting in the value 7.
	CE00C43D881120 finds the maximum of 7, 8, and 9, resulting in the value 9.
	D8005AC2A8F0 produces 1, because 5 is less than 15.
	F600BC2D8F produces 0, because 5 is not greater than 15.
	9C005AC2F8F0 produces 0, because 5 is not equal to 15.
	9C0141080250320F1802104A08 produces 1, because 1 + 3 = 2 * 2.
	*/
}
