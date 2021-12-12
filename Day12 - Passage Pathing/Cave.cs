using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day12;

public class Cave
{
	private readonly bool _bigCave;

	public string Name { get; }

	public bool IsBigCave => _bigCave;
	public bool IsSmallCave => !_bigCave;

	public Cave(string name)
	{
		Name = name;
		if (name.ToUpper().Equals(name))
		{
			_bigCave = true;
		}
		else if (name.ToLower().Equals(name))
		{
			_bigCave = false;
		}
		else throw new FormatException($"Cave name must be either all uppercase or all lowercase (was \"{name}\").");
	}

	public ISet<Cave> ConnectedCaves { get; } = new HashSet<Cave>();

	public override string ToString()
	{
		return Name;
	}
}
