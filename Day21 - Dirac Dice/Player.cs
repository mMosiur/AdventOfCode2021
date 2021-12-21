namespace AdventOfCode.Year2021.Day21;

public class Player
{
	public int Position { get; set; }
	public int Score { get; set; }

	public Player(int position)
	{
		Position = position;
		Score = 0;
	}
}
