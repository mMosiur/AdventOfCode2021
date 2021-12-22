using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2021.Day22;

public readonly struct Cuboid
{
	public Range SideX { get; }
	public Range SideY { get; }
	public Range SideZ { get; }

	public Cuboid(Range sideX, Range sideY, Range sideZ)
	{
		SideX = sideX;
		SideY = sideY;
		SideZ = sideZ;
	}

	public Cuboid(int xStart, int xEnd, int yStart, int yEnd, int zStart, int zEnd)
		: this(new Range(xStart, xEnd), new Range(yStart, yEnd), new Range(zStart, zEnd))
	{
	}

	public Cuboid? Intersect(Cuboid other)
	{
		int startX = Math.Max(SideX.Start, other.SideX.Start);
		int endX = Math.Min(SideX.End, other.SideX.End);
		if (endX < startX) return null;
		int startY = Math.Max(SideY.Start, other.SideY.Start);
		int endY = Math.Min(SideY.End, other.SideY.End);
		if (endY < startY) return null;
		int startZ = Math.Max(SideZ.Start, other.SideZ.Start);
		int endZ = Math.Min(SideZ.End, other.SideZ.End);
		if (endZ < startZ) return null;
		return new Cuboid(startX, endX, startY, endY, startZ, endZ);
	}

	public bool SplitOnIntersection(Cuboid other, [NotNullWhen(true)] out ICollection<Cuboid>? partsOutsideIntersection)
	{
		partsOutsideIntersection = null;
		Cuboid? intersectResult = Intersect(other);
		if (intersectResult is null) return false;
		Cuboid overlap = intersectResult.Value;
		partsOutsideIntersection = SplitOnIntersection(overlap);
		return true;
	}

	private ICollection<Cuboid> SplitOnIntersection(Cuboid overlap)
	{
		ICollection<Cuboid> partsOutsideIntersection = new List<Cuboid>();
		if (SideX.Start != overlap.SideX.Start)
		{
			partsOutsideIntersection.Add(new Cuboid(SideX.Start, overlap.SideX.Start - 1, SideY.Start, SideY.End, SideZ.Start, SideZ.End));
		}
		if (SideX.End != overlap.SideX.End)
		{
			partsOutsideIntersection.Add(new Cuboid(overlap.SideX.End + 1, SideX.End, SideY.Start, SideY.End, SideZ.Start, SideZ.End));
		}
		if (SideY.Start != overlap.SideY.Start)
		{
			partsOutsideIntersection.Add(new Cuboid(overlap.SideX.Start, overlap.SideX.End, SideY.Start, overlap.SideY.Start - 1, SideZ.Start, SideZ.End));
		}
		if (SideY.End != overlap.SideY.End)
		{
			partsOutsideIntersection.Add(new Cuboid(overlap.SideX.Start, overlap.SideX.End, overlap.SideY.End + 1, SideY.End, SideZ.Start, SideZ.End));
		}
		if (SideZ.Start != overlap.SideZ.Start)
		{
			partsOutsideIntersection.Add(new Cuboid(overlap.SideX.Start, overlap.SideX.End, overlap.SideY.Start, overlap.SideY.End, SideZ.Start, overlap.SideZ.Start - 1));
		}
		if (SideZ.End != overlap.SideZ.End)
		{
			partsOutsideIntersection.Add(new Cuboid(overlap.SideX.Start, overlap.SideX.End, overlap.SideY.Start, overlap.SideY.End, overlap.SideZ.End + 1, SideZ.End));
		}
		return partsOutsideIntersection;
	}

	public long Volume => (long)SideX.Length * (long)SideY.Length * (long)SideZ.Length;
}
