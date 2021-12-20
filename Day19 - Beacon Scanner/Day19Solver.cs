using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2021.Day19;

public class Day19Solver : DaySolver
{
	private readonly ScannerLocator _scannerLocator;

	public Day19Solver(string inputFilePath) : base(inputFilePath)
	{
		Regex regex = new Regex(@" *--- scanner (?<scanner>\d+) --- *\n(?<values>(?>-?\d+, *-?\d+, *-?\d+(?>\n|$))+)");
		List<Scanner> scanners = new();
		foreach (Match match in regex.Matches(Input))
		{
			int scanner = int.Parse(match.Groups["scanner"].ValueSpan);
			string values = match.Groups["values"].Value;
			using StringReader reader = new(values);
			string? line;
			HashSet<Point> positions = new();
			while ((line = reader.ReadLine()) is not null)
			{
				if (!positions.Add(Point.Parse(line)))
				{
					throw new InvalidDataException("Duplicate position");
				}
			}
			scanners.Add(new Scanner(scanner, positions));
		}
		_scannerLocator = new ScannerLocator(scanners, 0);
	}

	public override string SolvePart1()
	{
		_scannerLocator.LocateAndRotateScanners();
		int result = _scannerLocator.AbsoluteBeaconPositions.Count;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		_scannerLocator.LocateAndRotateScanners();
		List<Point> scannerPositions = _scannerLocator.Scanners.Select(s => s.Position).ToList();
		int maxDistance = int.MinValue;
		for (int i = 0; i < scannerPositions.Count; i++)
		{
			for (int j = i + 1; j < scannerPositions.Count; j++)
			{
				int distance = scannerPositions[i].ManhattanDistanceTo(scannerPositions[j]);
				if (distance > maxDistance)
				{
					maxDistance = distance;
				}
			}
		}
		return maxDistance.ToString();
	}
}
