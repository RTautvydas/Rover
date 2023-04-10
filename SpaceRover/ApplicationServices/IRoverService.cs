using SpaceRover.Models;

namespace SpaceRover.ApplicationServices;

/// <summary>
/// Represents the interface for the RoverService that can validate and execute commands for a rover.
/// </summary>
public interface IRoverService
{
    /// <summary>
    /// Determines whether the given command string is a valid command.
    /// </summary>
    /// <param name="commandLine">The string containing the command to validate.</param>
    /// <returns>True if the command is valid, false otherwise.</returns>
    bool IsCommandValid(string commandLine);

    /// <summary>
    /// Executes a command line on a rover and returns its final position on the grid.
    /// </summary>
    /// <param name="commandLine">A string representing the commands to be executed by the rover.</param>
    /// <returns>A <see cref="RoverPosition"/> object representing the final position of the rover on the grid.</returns>
    RoverPosition FindCoordinates(string commandLine);
}