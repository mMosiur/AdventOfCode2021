using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021.Day22;

public readonly struct ReactorCoreRebootStep
{
	private static readonly Regex _regex = new Regex(@"^[ \t]*((?>on)|(?>off))[ \t]+x[ \t]*=[ \t]*(-?\d+)[ \t]*..[ \t]*(-?\d+)[ \t]*,[ \t]*y[ \t]*=[ \t]*(-?\d+)[ \t]*..[ \t]*(-?\d+)[ \t]*,[ \t]*z[ \t]*=[ \t]*(-?\d+)[ \t]*..[ \t]*(-?\d+)[ \t]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	public CubeState CubeState { get; }

	public Cuboid Cuboid { get; }

	public ReactorCoreRebootStep(CubeState cubeState, Cuboid cuboid)
	{
		CubeState = cubeState;
		Cuboid = cuboid;
	}

	public ReactorCoreRebootStep(CubeState cubeState, Range sideX, Range sideY, Range sideZ)
		: this(cubeState, new Cuboid(sideX, sideY, sideZ))
	{
	}

	public ReactorCoreRebootStep(CubeState cubeState, int fromX, int toX, int fromY, int toY, int fromZ, int toZ)
		: this(cubeState, new Range(fromX, toX), new Range(fromY, toY), new Range(fromZ, toZ))
	{
	}

	public static ReactorCoreRebootStep Parse(string s)
	{
		Match match = _regex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Cannot parse {s}.");
		}
		string cubeStateString = match.Groups[1].Value;
		CubeState cubeState = cubeStateString.ToLower() switch
		{
			"on" => CubeState.On,
			"off" => CubeState.Off,
			_ => throw new FormatException($"Cannot parse {s}."),
		};
		int fromX = int.Parse(match.Groups[2].ValueSpan);
		int toX = int.Parse(match.Groups[3].ValueSpan);
		int fromY = int.Parse(match.Groups[4].ValueSpan);
		int toY = int.Parse(match.Groups[5].ValueSpan);
		int fromZ = int.Parse(match.Groups[6].ValueSpan);
		int toZ = int.Parse(match.Groups[7].ValueSpan);
		return new ReactorCoreRebootStep(cubeState, fromX, toX, fromY, toY, fromZ, toZ);
	}
}
