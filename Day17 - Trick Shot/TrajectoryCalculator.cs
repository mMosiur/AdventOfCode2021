using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day17;

public class TrajectoryCalculator
{
	public bool DoesHitTarget(Velocity initialVelocity, Target target)
	{
		Position position = Position.Origin;
		Velocity velocity = initialVelocity;
		while (true)
		{
			position += velocity;
			velocity = velocity.NextStep();

			if (!target.ContainsHorizontally(position))
			{
				if (velocity.X == 0)
				{
					// The X position will not change and never hits the target.
					return false;
				}
			}
			else if (!target.ContainsVertically(position))
			{
				if (velocity.Y < 0 && position.Y < target.MinY)
				{
					// The Y position is below the target and the velocity is negative, and will never hit the target.
					return false;
				}
			}
			else
			{
				return true;
			}
		}
	}

	public int HighestPossibleVerticalPosition(Target target)
	{
		// With initial positive vertical velocity of `y`, the velocity reaches 0 after `y` steps.
		// After that is accelerates downward and after next `y` steps, the velocity reaches 0 again (or the starting one).
		// At that point the vertical velocity  is `-y-1`.
		// And so as to not overshoot the target area in the next step, given `-y-1` at most (<=) targets minimal y (`minY`).
		// Consequently, `y` should be at least (>=) `-minY-1`.
		// And given that we look for maximal value of `y` (to get as fat up as possible), then `y` should be at the edge, equal to `-minY-1`.
		int y = -target.MinY - 1;
		// `y` here is the initial y velocity, now the height reached with it is equal to 1+2+3+...+y = (y+1)*y/2.
		int result = (y * (y + 1)) / 2;
		return result;
	}

	public IEnumerable<Velocity> InitialVelocitiesThatHitTarget(Target target)
	{
		for (int i = 0; i <= target.MaxX; i++)
		{
			for (int j = target.MinY; j < -target.MinY; j++)
			{
				Velocity velocity = new Velocity(i, j);
				if (DoesHitTarget(velocity, target))
				{
					yield return velocity;
				}
			}
		}
	}
}
