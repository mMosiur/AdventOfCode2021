using System;

namespace AdventOfCode.Year2021.Day18;

public class Number : PairElement
{
	public int Value { get; set; }

	public override int Magnitude => Value;

	public Number(int value)
	{
		Value = value;
	}

	public override object Clone()
	{
		return new Number(Value);
	}

	public void Split()
	{
		if (Parent is null) throw new InvalidOperationException();
		Pair pair = new Pair(new Number((int)Math.Floor(Value / 2.0)), new Number((int)Math.Ceiling(Value / 2.0)));
		if (ReferenceEquals(Parent.Left, this))
		{
			Parent.Left = pair;
		}
		else if (ReferenceEquals(Parent.Right, this))
		{
			Parent.Right = pair;
		}
		else throw new InvalidOperationException();
	}

	public override string ToString() => Value.ToString();
}
