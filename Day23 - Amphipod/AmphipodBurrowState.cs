namespace AdventOfCode.Year2021.Day23;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

public readonly record struct AmphipodBurrowState(string Hall, string Rooms, int RoomSize)
{
	private static Lazy<char[]> _amphipodTypes = new Lazy<char[]>(() =>
	{
		return AmphipodBurrow.RoomTypes.Select(t => (char)t).ToArray();
	});

	public static char[] AmphipodTypes => _amphipodTypes.Value;

	public string GetRoom(int index) => Rooms.Substring(index * RoomSize, RoomSize);

	public IEnumerable<(AmphipodBurrowState state, int cost)> NextPossibleStates()
	{
		for (int i = 0; i < AmphipodBurrow.RoomCount; i++)
		{
			foreach (int position in CorridorPositionAvailable(i))
			{
				if (TryMoveFromRoomToCorridor(i, position, out AmphipodBurrowState nextState, out char amphipod, out int steps))
				{
					int energy = steps * Amphipod.GetEnergyPerStep((AmphipodType)amphipod);
					yield return (nextState, energy);
				}
			}
		}

		for (var i = 0; i < Hall.Length; i++)
		{
			if (TryMoveFromCorridorToRoom(i, out AmphipodBurrowState nextState, out char amphipod, out int steps))
			{
				int energy = steps * Amphipod.GetEnergyPerStep((AmphipodType)amphipod);
				yield return (nextState, energy);
			}
		}
	}

	private bool TryMoveFromRoomToCorridor(int roomIndex, int targetPosition, out AmphipodBurrowState nextState, out char amphipod, out int steps)
	{
		string room = GetRoom(roomIndex);
		int positionInRoom = room.IndexOfAny(AmphipodTypes);
		if (positionInRoom < 0)
		{
			// Room was empty
			nextState = default;
			amphipod = default;
			steps = default;
			return false;
		}

		steps = Math.Abs(targetPosition - AmphipodBurrow.RoomPositions[roomIndex]) + positionInRoom + 1;
		amphipod = room[positionInRoom];

		string newHall = Hall.Remove(targetPosition, 1).Insert(targetPosition, amphipod.ToString());
		string newRoom = room.Remove(positionInRoom, 1).Insert(positionInRoom, ".");
		string newRooms = Rooms.Remove(roomIndex * RoomSize, RoomSize).Insert(roomIndex * RoomSize, newRoom);
		nextState = this with { Hall = newHall, Rooms = newRooms };
		return true;
	}

	private bool TryMoveFromCorridorToRoom(int corridorPosition, out AmphipodBurrowState nextState, out char amphipod, out int steps)
	{
		nextState = default;
		steps = default;

		amphipod = Hall[corridorPosition];
		int destinationRoomIndex = Array.IndexOf(AmphipodTypes, amphipod);
		if (destinationRoomIndex < 0)
			return false;

		int destinationPosition = AmphipodBurrow.RoomPositions[destinationRoomIndex];
		int start = destinationPosition > corridorPosition ? corridorPosition + 1 : corridorPosition - 1;
		int min = Math.Min(destinationPosition, start);
		int max = Math.Max(destinationPosition, start);
		if (Hall.Skip(min).Take(max - min + 1).Any(ch => ch != '.'))
			return false;

		string room = GetRoom(destinationRoomIndex);
		char type = amphipod;
		if (room.Any(ch => ch != '.' && ch != type))
			return false;

		int depth = room.LastIndexOf('.');
		steps = (max - min + 1) + depth + 1;

		string newHall = Hall.Remove(corridorPosition, 1).Insert(corridorPosition, ".");
		string newRoom = room.Remove(depth, 1).Insert(depth, amphipod.ToString());
		string newRooms = Rooms.Remove(destinationRoomIndex * RoomSize, RoomSize).Insert(destinationRoomIndex * RoomSize, newRoom);
		nextState = this with { Hall = newHall, Rooms = newRooms };
		return true;
	}

	private IEnumerable<int> CorridorPositionAvailable(int startingRoomIndex)
	{
		int roomPosition = AmphipodBurrow.RoomPositions[startingRoomIndex];
		for (int i = roomPosition - 1; i >= 0; --i)
		{
			if (Hall[i] != '.') break;
			if (!AmphipodBurrow.RoomPositions.Contains(i)) yield return i;
		}

		for (int i = roomPosition + 1; i < Hall.Length; ++i)
		{
			if (Hall[i] != '.') break;
			if (!AmphipodBurrow.RoomPositions.Contains(i)) yield return i;
		}
	}
}
