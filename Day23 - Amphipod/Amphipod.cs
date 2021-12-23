using System;

namespace AdventOfCode.Year2021.Day23;

public class Amphipod
{
	public AmphipodType Type { get; }

	public int EnergyPerStep => GetEnergyPerStep(Type);

	public Amphipod(AmphipodType type)
	{
		Type = type;
	}

	public Amphipod(char type)
	{
		if (!Enum.IsDefined(typeof(AmphipodType), (ushort)type))
			throw new ArgumentException($"Invalid amphipod type: '{type}'");
		Type = (AmphipodType)type;
	}

	public static Amphipod? FromChar(char c)
	{
		if (c == '.') return null;
		return new Amphipod(c);
	}

	public static int GetEnergyPerStep(AmphipodType type) => type switch
	{
		AmphipodType.Amber => 1,
		AmphipodType.Bronze => 10,
		AmphipodType.Copper => 100,
		AmphipodType.Desert => 1000,
		_ => throw new ArgumentException($"No amphipod with character representation '{type}'.", nameof(type))
	};
}

public enum AmphipodType : ushort
{
	Amber = 'A',
	Bronze = 'B',
	Copper = 'C',
	Desert = 'D'
}
