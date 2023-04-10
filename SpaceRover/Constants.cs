namespace SpaceRover;

/// <summary>
/// Constants used throughout the application.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Name of the configuration section
    /// </summary>
    public const string ConfigurationSection = "Settings";
    
    /// <summary>
    /// Keys used for exceptions in the application.
    /// </summary>
    public static class ExceptionKeys
    {
        /// <summary>
        /// Invalid command key
        /// </summary>
        public const string Command = nameof(Command);
        
        /// <summary>
        /// Step key
        /// </summary>
        public const string Step = nameof(Step);
    }
}