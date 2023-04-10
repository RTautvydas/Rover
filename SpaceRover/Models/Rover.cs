namespace SpaceRover.Models;

/// <summary>
/// Represents a rover that can move and rotate on a two-dimensional surface.
/// </summary>
public class Rover
{
    /// <summary>
    /// Gets or sets the commands that the rover should execute.
    /// </summary>
    public Commands Commands { get; init; }

    /// <summary>
    /// Gets or sets the size of each step the rover takes when moving.
    /// </summary>
    public double StepSize { get; init; }

    /// <summary>
    /// Gets or sets the initial position of the rover on the surface.
    /// </summary>
    public Position Position { get; init; }
}