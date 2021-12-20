using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day18;

public class Pair : PairElement
{
	private PairElement _left;
	private PairElement _right;
	public PairElement Left
	{
		get => _left;
		set
		{
			_left = value;
			_left.Parent = this;
		}
	}
	public PairElement Right
	{
		get => _right;
		set
		{
			_right = value;
			_right.Parent = this;
		}
	}

	public override int Magnitude => 3 * Left.Magnitude + 2 * Right.Magnitude;

	public Pair(PairElement left, PairElement right)
	{
		_left = left;
		_left.Parent = this;
		_right = right;
		_right.Parent = this;
	}

	public static Pair Parse(string s)
	{
		Stack<char> bracketStack = new Stack<char>();
		Stack<PairElement> stack = new Stack<PairElement>();
		Number? number = null;
		foreach (char c in s)
		{
			if (c == ']')
			{
				if (number is not null)
				{
					stack.Push(number);
					number = null;
				}
				if (!bracketStack.TryPop(out char bracket) || bracket != '[')
				{
					throw new FormatException("Invalid input");
				}
				var right = stack.Pop();
				var left = stack.Pop();
				stack.Push(new Pair(left, right));
			}
			else if (c == '[')
			{
				bracketStack.Push(c);
			}
			else if (c == ',')
			{
				if (number is not null)
				{
					stack.Push(number);
					number = null;
				}
			}
			else if (char.IsDigit(c))
			{
				int digit = (int)char.GetNumericValue(c);
				if (number is null)
				{
					number = new Number(digit);
				}
				else
				{
					number.Value = number.Value * 10 + digit;
				}
			}
			else
			{
				throw new FormatException("Invalid character");
			}
		}
		if (bracketStack.Count > 0)
		{
			throw new FormatException("Invalid input");
		}
		Pair pair = stack.Single() as Pair ?? throw new FormatException("Invalid format");
		return pair;
	}

	public int Reduce()
	{
		bool reduced = true;
		int steps = 0;
		while (true)
		{
			reduced = ReduceStep();
			if (!reduced) break;
			steps++;
		}
		return steps;
	}

	private bool ReduceStep()
	{
		Pair? p = FindLeftmostPairAtGivenDepth(4);
		if (p is not null)
		{
			p.Explode();
			return true;
		}
		Number? n = FindLeftmostNumberEqualToOrGreaterThan(10);
		if (n is not null)
		{
			n.Split();
			return true;
		}
		return false;
	}

	private void Explode()
	{
		if (Left is not Number leftNumber || Right is not Number rightNumber) throw new InvalidOperationException();
		if (Parent is null) throw new InvalidOperationException();
		// Add to left
		Pair? p = this;
		while (p.Parent is not null && ReferenceEquals(p.Parent.Left, p))
		{
			p = p.Parent;
		}
		p = p.Parent;
		if (p is not null)
		{
			if (p.Left is Number pLeftNumber)
			{
				pLeftNumber.Value += leftNumber.Value;
			}
			else
			{
				p = (Pair)p.Left;
				while (p.Right is Pair pRightPair)
				{
					p = pRightPair;
				}
				((Number)p.Right).Value += leftNumber.Value;
			}
		}
		// Add to right
		p = this;
		while (p.Parent is not null && ReferenceEquals(p.Parent.Right, p))
		{
			p = p.Parent;
		}
		p = p.Parent;
		if (p is not null)
		{
			if (p.Right is Number pRightNumber)
			{
				pRightNumber.Value += rightNumber.Value;
			}
			else
			{
				p = (Pair)p.Right;
				while (p.Left is Pair pLeftPair)
				{
					p = pLeftPair;
				}
				((Number)p.Left).Value += rightNumber.Value;
			}
		}
		// Replace with 0
		if (ReferenceEquals(Parent.Left, this))
		{
			Parent.Left = new Number(0);
		}
		else if (ReferenceEquals(Parent.Right, this))
		{
			Parent.Right = new Number(0);
		}
		else throw new InvalidOperationException();
		Parent = null;
	}

	private Pair? FindLeftmostPairAtGivenDepth(int depth)
	{
		if (depth < 0) throw new ArgumentOutOfRangeException();
		if (depth == 0) return this;
		if (Left is Pair leftPair)
		{
			Pair? p = leftPair.FindLeftmostPairAtGivenDepth(depth - 1);
			if (p is not null) return p;
		}
		if (Right is Pair rightPair)
		{
			Pair? p = rightPair.FindLeftmostPairAtGivenDepth(depth - 1);
			if (p is not null) return p;
		}
		return null;
	}

	private Number? FindLeftmostNumberEqualToOrGreaterThan(int number)
	{
		if (Left is Number leftNumber)
		{
			if (leftNumber.Value >= number) return leftNumber;
		}
		else
		{
			Pair leftPair = (Pair)Left;
			Number? n = leftPair.FindLeftmostNumberEqualToOrGreaterThan(number);
			if (n is not null) return n;
		}
		if (Right is Number rightNumber)
		{
			if (rightNumber.Value >= number) return rightNumber;
		}
		else
		{
			Pair rightPair = (Pair)Right;
			Number? n = rightPair.FindLeftmostNumberEqualToOrGreaterThan(number);
			if (n is not null) return n;
		}
		return null;
	}

	public override object Clone()
	{
		return new Pair((PairElement)Left.Clone(), (PairElement)Right.Clone());
	}

	public override string ToString() => $"[{Left},{Right}]";
}
