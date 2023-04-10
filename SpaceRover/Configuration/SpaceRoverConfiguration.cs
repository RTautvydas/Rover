using SpaceRover.Models;

namespace SpaceRover.Configuration;

/// <summary>
/// Represents the configuration of a space rover.
/// </summary>
public class SpaceRoverConfiguration
{
    /// <summary>
    /// Gets or sets the rover in the configuration.
    /// </summary>
    public Rover Rover { get; init; }
    
    /// <summary>
    /// Gets or sets the grid in the configuration.
    /// </summary>
    public Grid Grid { get; init; }
}