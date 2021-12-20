using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day19;

public class Scanner
{
	public int Number { get; }
	public IReadOnlySet<Point> RelativeBeaconPositions { get; private set; }

	public Point Position { get; private set; }

	public Scanner(int number, IReadOnlySet<Point> beaconPositions)
	{
		Number = number;
		RelativeBeaconPositions = beaconPositions;
		Position = Point.Origin;
	}

	public IEnumerable<IReadOnlySet<Point>> BeaconPositionRotations
	{
		get
		{
			List<PointRotations> rotations = RelativeBeaconPositions.Select(p => new PointRotations(p)).ToList();
			for (int i = 0; i < PointRotations.CountOfRotations; i++)
			{
				yield return rotations.Select(r => r[i]).ToHashSet();
			}
		}
	}

	public void AdjustScanner(IReadOnlySet<Point> beaconPositions, Vector offset)
	{
		RelativeBeaconPositions = beaconPositions;
		Position += offset;
	}
}
