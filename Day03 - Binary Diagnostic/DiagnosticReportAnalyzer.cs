using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2021.Day03;

public class DiagnosticReportAnalyzer
{
	public IList<int> DiagnosticReport { get; }

	private readonly int _maxBinaryLength;
	private readonly Lazy<(int GammaRate, int EpsilonRate)> _gammaAndEpsilonRates;
	private readonly Lazy<int> _oxygenGeneratorRating;
	private readonly Lazy<int> _co2ScrubberRating;

	public DiagnosticReportAnalyzer(IList<int> diagnosticReport)
	{
		DiagnosticReport = diagnosticReport;
		_maxBinaryLength = DiagnosticReport.Max(number => (int)Math.Log2(number) + 1);
		_gammaAndEpsilonRates = new(CalculateGammaAndEpsilonRates);
		_oxygenGeneratorRating = new(CalculateOxygenGeneratorRating);
		_co2ScrubberRating = new(CalculateCO2ScrubberRating);
	}

	private (int GammaRate, int EpsilonRate) CalculateGammaAndEpsilonRates()
	{
		int gammaRate = 0;
		int epsilonRate = 0;
		for (int i = _maxBinaryLength - 1; i >= 0; i--)
		{
			int[] counts = new int[2];
			foreach (int number in DiagnosticReport)
			{
				counts[number.GetBit(i)]++;
			}
			switch (counts[1] - counts[0])
			{
				case > 0:
					gammaRate += 1 << i;
					break;
				case < 0:
					epsilonRate += 1 << i;
					break;
				default:
					throw new InvalidDataException("Equal number of ones and zeros was not expected.");
			}
		}
		return (gammaRate, epsilonRate);
	}

	private int CalculateFilterBasedRating(Func<int, int, bool> bitSelector)
	{
		IList<int> numbers = DiagnosticReport;
		for (int i = _maxBinaryLength - 1; i >= 0; i--)
		{
			int[] counts = new int[2];
			foreach (int number in numbers)
			{
				counts[number.GetBit(i)]++;
			}
			int filterBit = bitSelector(counts[0], counts[1]) ? 0 : 1;
			numbers = numbers.Where(n => n.GetBit(i) == filterBit).ToList();
			if (numbers.Count == 1)
			{
				return numbers.Single();
			}
		}
		throw new InvalidDataException("The number list did not reach a state with only a single number left.");
	}

	private int CalculateOxygenGeneratorRating()
		=> CalculateFilterBasedRating((zerosCount, onesCount) => zerosCount <= onesCount);

	private int CalculateCO2ScrubberRating()
		=> CalculateFilterBasedRating((zerosCount, onesCount) => zerosCount > onesCount);

	public int GammaRate => _gammaAndEpsilonRates.Value.GammaRate;
	public int EpsilonRate => _gammaAndEpsilonRates.Value.EpsilonRate;
	public int PowerConsumption => GammaRate * EpsilonRate;

	public int OxygenGeneratorRating => _oxygenGeneratorRating.Value;
	public int CO2ScrubberRating => _co2ScrubberRating.Value;
	public int LifeSupportRating => OxygenGeneratorRating * CO2ScrubberRating;
}
