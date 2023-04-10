using System.Text.Json.Serialization;
using SpaceRover.Enums;

namespace SpaceRover.Models;

/// <summary>
/// Position
/// </summary>
public class Position : IPosition
{
    /// <summary>
    /// Gets or sets the x-coordinate of the rover's position.
    /// </summary>
    public double X { get; protected set; }  
    
    /// <summary>
    /// Gets or sets the y-coordinate of the rover's position.
    /// </summary>
    public double Y { get; protected set; }
    
    /// <summary>
    /// Gets or sets the direction the rover is facing.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Direction Direction { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> class
    /// with the specified coordinates and direction.
    /// </summary>
    /// <param name="x">The X coordinate of the position.</param>
    /// <param name="y">The Y coordinate of the position.</param>
    /// <param name="direction">The direction of the position.</param>
    public Position(double x, double y, Direction direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }
}