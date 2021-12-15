using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day15;

public class CaveSegment
{
	public int RiskLevel { get; }
	public ISet<CaveSegment> AdjacentSegments { get; } = new HashSet<CaveSegment>();

	public CaveSegment(int riskLevel)
	{
		RiskLevel = riskLevel;
	}
}
