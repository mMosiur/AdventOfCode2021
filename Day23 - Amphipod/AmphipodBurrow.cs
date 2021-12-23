using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day23;

public class AmphipodBurrow
{
	public static int RoomCount { get; } = 4;
	public static IReadOnlyList<int> RoomPositions { get; } = new int[] { 2, 4, 6, 8 };
	public static IReadOnlyList<AmphipodType> RoomTypes { get; } = new AmphipodType[] {
		AmphipodType.Amber,
		AmphipodType.Bronze,
		AmphipodType.Copper,
		AmphipodType.Desert
	};

	public IReadOnlyList<Amphipod?> Corridor { get; }
	public IReadOnlyList<IReadOnlyList<Amphipod?>> Rooms { get; }
	public int RoomSize { get; }

	public AmphipodBurrow(IReadOnlyList<Amphipod?> corridor, IReadOnlyList<IReadOnlyList<Amphipod?>> rooms, int roomSize)
	{
		Corridor = corridor;
		Rooms = rooms;
		RoomSize = roomSize;
	}

	public AmphipodBurrowState State
	{
		get
		{
			string hallStr = string.Concat(Corridor.Select(amphipod => ((char?)amphipod?.Type) ?? '.'));
			string roomsStr = string.Concat(
				Rooms.Select(room =>
					string.Concat(
						room.Select(amphipod => ((char?)amphipod?.Type) ?? '.')
					)
				)
			);
			return new AmphipodBurrowState(hallStr, roomsStr, RoomSize);
		}
	}

	public static AmphipodBurrow Parse(IEnumerable<string> lines)
	{
		const int NumberOfRooms = 4;
		const int CorridorLength = 11;
		Action<bool> AssertFormat = condition =>
		{
			if (!condition) throw new FormatException("Input was in an invalid format.");
		};
		var it = lines.GetEnumerator();
		AssertFormat(it.MoveNext());
		AssertFormat(it.Current.All(c => c == '#'));
		AssertFormat(it.MoveNext());
		string hall = it.Current.Trim('#');
		AssertFormat(hall.Length == CorridorLength && hall.All(c => c == '.'));
		string[] rooms = new string[NumberOfRooms] { "", "", "", "" };
		while (it.MoveNext() && !it.Current.Trim().All(c => c == '#'))
		{
			string[] roomLevel = it.Current.Trim().Trim('#').Split('#');
			AssertFormat(roomLevel.Length == NumberOfRooms && roomLevel.All(r => r.Length == 1));
			for (int i = 0; i < NumberOfRooms; i++)
			{
				rooms[i] += roomLevel[i];
			}
		}
		AssertFormat(it.Current.Trim().All(c => c == '#'));
		int roomSize = rooms[0].Length;
		AssertFormat(rooms.All(r => r.Length == roomSize));
		return new AmphipodBurrow(
			hall.Select(Amphipod.FromChar).ToList(),
			rooms.Select(room => room.Select(Amphipod.FromChar).ToList()).ToList(),
			roomSize
		);
	}
}
