namespace SpaceRover.Models;

/// <summary>
/// Represents the commands that a rover can execute.
/// </summary>
public class Commands : ICommands
{
    /// <summary>
    /// Gets or sets the character that represents moving forward.
    /// </summary>
    public char Forward { get; set; }
    
    /// <summary>
    /// Gets or sets the character that represents moving backwards.
    /// </summary>
    public char Backwards { get; set; }
    
    /// <summary>
    /// Gets or sets the character that represents rotating left.
    /// </summary>
    public char Left { get; set; }
    
    /// <summary>
    /// Gets or sets the character that represents rotating right.
    /// </summary>
    public char Right { get; set; }
    
    /// <summary>
    /// Gets the regular expression pattern that matches the valid commands.
    /// </summary>
    public string RegexPattern => $"^[{Forward}{Backwards}{Left}{Right}]+$";
}