using System;

namespace AdventOfCode.Year2021.Day18;

public abstract class PairElement : ICloneable
{
	public abstract int Magnitude { get; }

	public abstract object Clone();

	public Pair? Parent { get; set;} = null;

	public static Pair operator +(PairElement left, PairElement right)
	{
		Pair sum = new Pair((PairElement)left.Clone(), (PairElement)right.Clone());
		sum.Reduce();
		return sum;
	}
}
