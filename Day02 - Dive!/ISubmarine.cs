namespace AdventOfCode.Year2021.Day02;

public interface ISubmarine
{
	public int HorizontalPosition { get; }
	public int Depth { get; }

	public void ExecuteCommand(Command command);
}
