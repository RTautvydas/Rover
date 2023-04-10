using Microsoft.Extensions.Options;
using SpaceRover.Configuration;

namespace SpaceRover.Helpers;

/// <summary>
/// This class contains helper methods for validating configuration options.
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// ValidateDirections method checks if the rover's commands for movement are unique.
    /// </summary>
    private static readonly Func<SpaceRoverConfiguration, bool> ValidateDirections = x =>
        x.Rover.Commands.Forward != x.Rover.Commands.Backwards &&
        x.Rover.Commands.Forward != x.Rover.Commands.Left &&
        x.Rover.Commands.Forward != x.Rover.Commands.Right &&
        x.Rover.Commands.Backwards != x.Rover.Commands.Left &&
        x.Rover.Commands.Backwards != x.Rover.Commands.Right &&
        x.Rover.Commands.Left != x.Rover.Commands.Right;

    /// <summary>
    /// Checks if the rover's commands for movement have been set.
    /// </summary>
    private static readonly Func<SpaceRoverConfiguration, bool> ValidateDefaultDirections = x =>
        x.Rover.Commands.Forward != default(char) &&
        x.Rover.Commands.Backwards != default(char) &&
        x.Rover.Commands.Left != default(char) && 
        x.Rover.Commands.Right != default(char);

    /// <summary>
    /// Checks if the grid size is valid (i.e. height and width are greater than 0).
    /// </summary>
    private static readonly Func<SpaceRoverConfiguration, bool> ValidateGrid = x => x.Grid is { Height: > 0, Width: > 0 };

    /// <summary>
    /// Checks if the rover's starting position is valid (i.e. X and Y positions are greater than or equal to 0).
    /// </summary>
    private static readonly Func<SpaceRoverConfiguration, bool> ValidatePosition = x => x.Rover.Position is { X: >= 0, Y: >= 0 };

    /// <summary>
    /// Checks if the step size is greater than 0.
    /// </summary>
    private static readonly Func<SpaceRoverConfiguration, bool> ValidateStep = x => x.Rover.StepSize > 0;

    /// <summary>
    /// Adds configuration validations to the options builder.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    /// <returns>The options builder with added configuration validations.</returns>
    public static OptionsBuilder<SpaceRoverConfiguration> AddConfigurationValidations(this OptionsBuilder<SpaceRoverConfiguration> optionsBuilder) =>
        optionsBuilder
            .Validate(ValidateGrid, "Grid height or width can not be negative")
            .Validate(ValidateDirections, "Direction commands must be unique")
            .Validate(ValidateDefaultDirections, "Direction commands must be set")
            .Validate(ValidatePosition, "Rover X and Y position can not be negative")
            .Validate(ValidateStep, "Step size must be greater than 0");
}