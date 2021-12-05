using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day05;

public class VentAnalyzer
{
	private ICollection<VentLine> _ventLines;

	public VentAnalyzer(ICollection<VentLine> ventLines)
	{
		_ventLines = ventLines;
	}

	private IEnumerable<Point> OverlapingPoints(IEnumerable<VentLine> ventLines)
	{
		Dictionary<Point, int> pointCounts = new Dictionary<Point, int>();
		foreach (Point point in ventLines.SelectMany(vl => vl.Points))
		{
			if (!pointCounts.ContainsKey(point))
			{
				pointCounts.Add(point, 1);
			}
			else
			{
				pointCounts[point]++;
			}
		}
		return pointCounts.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key);
	}

	public IEnumerable<Point> OverlapingPointsOfNonDiagonalLines()
	{
		return OverlapingPoints(_ventLines.Where(v => v.IsHorizontal || v.IsVertical));
	}

	public IEnumerable<Point> OverlapingPoints()
	{
		return OverlapingPoints(_ventLines);
	}
}
