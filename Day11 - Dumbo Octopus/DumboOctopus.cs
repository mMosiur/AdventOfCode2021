namespace AdventOfCode.Year2021.Day11;

public class DumboOctopus
{
	private int _energyLevel;

	public int EnergyLevel => _energyLevel;

	public bool Flashed { get; private set; }

	public bool Step()
	{
		if (Flashed)
		{
			return false;
		}
		_energyLevel++;
		if (_energyLevel == 10)
		{
			Flashed = true;
			_energyLevel = 0;
			return true;
		}
		return false;
	}

	public void ClearFlash()
	{
		Flashed = false;
	}

	public DumboOctopus(int energyLevel)
	{
		_energyLevel = energyLevel;
	}
}
