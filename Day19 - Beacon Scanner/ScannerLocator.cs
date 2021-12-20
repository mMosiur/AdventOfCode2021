using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day19;

public class ScannerLocator
{
	public IReadOnlyCollection<Scanner> Scanners { get; }
	public IReadOnlyCollection<Point> AbsoluteBeaconPositions => _absoluteBeaconPositions.Value;

	private Scanner comparisonBaseScanner;
	private bool _located = false;
	private readonly Lazy<IReadOnlyCollection<Point>> _absoluteBeaconPositions;

	public ScannerLocator(IReadOnlyCollection<Scanner> scanners, int comparisonBaseScannerNumber)
	{
		Scanners = scanners;
		try
		{
			comparisonBaseScanner = Scanners.Single(s => s.Number == comparisonBaseScannerNumber);
		}
		catch (InvalidOperationException e)
		{
			throw new ArgumentException("There should be exactly one scanner with given number.", nameof(comparisonBaseScanner), e);
		}
		_absoluteBeaconPositions = new Lazy<IReadOnlyCollection<Point>>(CalculateBeaconPositions);
	}

	private int CountOverlapsAfterTranslation(IReadOnlySet<Point> pivotPointSet, IReadOnlySet<Point> rotatingPointSet, Vector translationVector)
	{
		int overlaps = rotatingPointSet.Select(p => p + translationVector).Count(pivotPointSet.Contains);
		return overlaps;
	}

	private Vector? FindTranslationVector(IReadOnlySet<Point> pivotPointSet, IReadOnlySet<Point> rotatingPointSet)
	{
		foreach ((Point pos1, int index) in pivotPointSet.Select((p, i) => (p, i)))
		{
			foreach (Point pos2 in rotatingPointSet.Take(index))
			{
				Vector diff = pos1 - pos2;
				int overlapCount = CountOverlapsAfterTranslation(pivotPointSet, rotatingPointSet, diff);
				if (overlapCount >= 12)
				{
					return diff;
				}
			}
		}
		return null;
	}

	private (IReadOnlySet<Point> Positions, Vector Offset)? FindMatchingTranslation(Scanner locatedScanner, Scanner scanner)
	{
		foreach (IReadOnlySet<Point> rotation in scanner.BeaconPositionRotations)
		{
			Vector? offset = FindTranslationVector(locatedScanner.RelativeBeaconPositions, rotation);
			if (offset is not null)
			{
				IReadOnlySet<Point> positions = rotation.Select(p => p + offset.Value).ToHashSet();
				return (positions, offset.Value);
			}
		}
		return null;
	}

	private void LocateAndRotateScannersStep(ICollection<Scanner> scannersLeft, ICollection<Scanner> locatedScanners)
	{
		foreach (Scanner scanner in scannersLeft)
		{
			foreach (Scanner locatedScanner in locatedScanners)
			{
				(IReadOnlySet<Point> Positions, Vector Offset)? match = FindMatchingTranslation(locatedScanner, scanner);
				if (match is not null)
				{
					scanner.AdjustScanner(match.Value.Positions, match.Value.Offset);
					locatedScanners.Add(scanner);
					scannersLeft.Remove(scanner);
					return;
				}
			}
		}
		throw new InvalidOperationException("Could not locate all scanners.");
	}

	public void LocateAndRotateScanners()
	{
		if (_located) return;
		IEnumerable<Scanner> comparisonBaseScannerEnumerable = Enumerable.Repeat(comparisonBaseScanner, 1);
		ICollection<Scanner> scannersLeft = new LinkedList<Scanner>(Scanners.Except(comparisonBaseScannerEnumerable));
		ICollection<Scanner> locatedScanners = new LinkedList<Scanner>(comparisonBaseScannerEnumerable);
		while (scannersLeft.Count > 0)
		{
			LocateAndRotateScannersStep(scannersLeft, locatedScanners);
		}
		_located = true;
	}

	private IReadOnlyCollection<Point> CalculateBeaconPositions()
	{
		if (!_located) throw new InvalidOperationException("Scanners have not been located yet.");
		HashSet<Point> positions = new();
		foreach (Scanner scanner in Scanners)
		{
			positions.UnionWith(scanner.RelativeBeaconPositions);
		}
		return positions;
	}
}
