using Resources;
using SpaceRover.Enums;

namespace SpaceRover.Models;

/// <summary>
/// Represents the current position of a rover on a 2D grid.
/// </summary>
public class RoverPosition : Position
{
    /// <summary>
    /// Grid settings
    /// </summary>
    private readonly Grid _grid;

    /// <summary>
    /// Gets the distance the rover moves with each step.
    /// </summary>
    private readonly double _stepSize;
    
    /// <summary>
    /// Initializes a new instance of the CurrentPosition class with the specified values.
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <param name="stepSize">The distance the rover moves with each step.</param>
    /// <param name="grid">Grid settings</param>
    public RoverPosition(IPosition position, double stepSize, Grid grid) : base(position.X, position.Y, position.Direction)
    {
        _stepSize = stepSize;
        _grid = grid;
    }

    /// <summary>
    /// Moves the rover vertically (north or south) by one step.
    /// </summary>
    /// <param name="isForward">True if the rover moves forward (north), false if the rover moves backward (south).</param>
    public void MoveVertically(bool isForward) => Y += GetCalculatedMoveValue(isForward);
    
    /// <summary>
    /// Moves the rover horizontally (east or west) by one step.
    /// </summary>
    /// <param name="isForward">True if the rover moves forward (east), false if the rover moves backward (west).</param>
    public void MoveHorizontally(bool isForward) => X += GetCalculatedMoveValue(isForward);

    /// <summary>
    /// Checks if the rover has moved out of the grid and throws an exception if it has.
    /// </summary>
    public void OnAfterMove()
    {
        const double min = 0;
        
        var isXPositionInvalid = IsOutOfBounds(min, _grid.Width, X);
        var isYPositionInvalid = IsOutOfBounds(min, _grid.Height, Y);
        
        if (isXPositionInvalid || isYPositionInvalid)
        {
            throw new InvalidOperationException(string.Format(Resource.InvalidPosition, isXPositionInvalid ? nameof(X) : nameof(Y)));
        }
    }
    
    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="RoverPosition"/> instance.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current instance.</param>
    /// <returns>true if the specified <see cref="object"/> is equal to the current instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        var position = obj as RoverPosition;
        return position != null &&
               _stepSize.Equals(position._stepSize) && 
               X.Equals(position.X) && 
               Y.Equals(position.Y) &&
               Direction == position.Direction;
    }

    /// <summary>
    /// Determines whether a given value is outside a specified range.
    /// </summary>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <param name="value">The value to check.</param>
    /// <returns>true if the value is outside the range; otherwise, false.</returns>
    private static bool IsOutOfBounds(double min, double max, double value) => value < min || value > max;

    /// <summary>
    /// Turns the rover to face a new direction.
    /// </summary>
    /// <param name="direction">The new direction for the rover to face.</param>
    public void Turn(Direction direction) => Direction = direction;

    /// <summary>
    /// Calculates the distance to move the rover based on the given direction.
    /// </summary>
    /// <param name="isForward">True if the rover moves forward, false if the rover moves backward.</param>
    /// <returns>The distance to move the rover.</returns>
    private double GetCalculatedMoveValue(bool isForward) => isForward ? _stepSize : -1 * _stepSize;
}