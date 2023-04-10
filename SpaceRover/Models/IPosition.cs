using SpaceRover.Enums;

namespace SpaceRover.Models;

/// <summary>
/// Represents a position in a two-dimensional space.
/// </summary>
public interface IPosition
{
    /// <summary>
    /// Gets the X coordinate of the position.
    /// </summary>
    double X { get; }
    
    /// <summary>
    /// Gets the Y coordinate of the position.
    /// </summary>
    double Y { get; }
    
    /// <summary>
    /// Gets the direction that the position is facing.
    /// </summary>
    Direction Direction { get; }
}