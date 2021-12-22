using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021.Day22;

public class ReactorCore
{
	private List<Cuboid> _cuboidsOn = new();

	public void SwitchOffAllCubes()
	{
		_cuboidsOn.Clear();
	}

	public void Reboot(IList<ReactorCoreRebootStep> steps)
	{
		SwitchOffAllCubes();
		foreach (ReactorCoreRebootStep step in steps)
		{
			List<int> indexesToRemove = new();
			List<Cuboid> cuboidsToAdd = new();
			if (step.CubeState == CubeState.On)
			{
				cuboidsToAdd.Add(step.Cuboid);
			}
			foreach ((int index, Cuboid cuboid) in _cuboidsOn.Select((c, i) => (i, c)))
			{
				if (cuboid.SplitOnIntersection(step.Cuboid, out ICollection<Cuboid>? partsOutsideIntersection))
				{
					indexesToRemove.Add(index);
					cuboidsToAdd.AddRange(partsOutsideIntersection);
				}
			}
			foreach (int index in indexesToRemove.AsEnumerable().Reverse())
			{
				_cuboidsOn.RemoveAt(index);
			}
			_cuboidsOn.AddRange(cuboidsToAdd);
		}
	}

	public long CountOnCubes()
	{
		return _cuboidsOn.Sum(c => c.Volume);
	}

	public long CountOnCubesInRegion(Cuboid region)
	{
		return _cuboidsOn
			.Select(c => c.Intersect(region))
			.Where(c => c is not null)
			.Sum(c => c!.Value.Volume);
	}
}
