namespace SpaceRover.Models;

/// <summary>
/// Defines the commands that a rover can execute.
/// </summary>
public interface ICommands
{
    /// <summary>
    /// Gets the character that represents moving forward.
    /// </summary>
    char Forward { get; }
    
    /// <summary>
    /// Gets the character that represents moving backwards.
    /// </summary>
    char Backwards { get; }
    
    /// <summary>
    /// Gets the character that represents rotating left.
    /// </summary>
    char Left { get; }
    
    /// <summary>
    /// Gets the character that represents rotating right.
    /// </summary>
    char Right { get; }
}