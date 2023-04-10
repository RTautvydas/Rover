using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using SpaceRover.Configuration;
using SpaceRover.Enums;
using SpaceRover.Helpers;
using SpaceRover.Models;

namespace SpaceRover.ApplicationServices;

/// <summary>
/// Service for handling rover commands and movements.
/// </summary>
public class RoverService : IRoverService
{
    /// <summary>
    /// Options
    /// </summary>
    private readonly SpaceRoverConfiguration _options;

    /// <summary>
    /// Actions
    /// </summary>
    private readonly Dictionary<(Direction, char), (Action<RoverPosition> execute, Action<RoverPosition>?onAfterExecute)> _actions;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoverService"/> class.
    /// </summary>
    /// <param name="options">The options containing the rover configuration.</param>
    public RoverService(IOptions<SpaceRoverConfiguration> options)
    {
        _options = options.Value;
        _actions = GetActions(_options.Rover.Commands);
    }

    /// <summary>
    /// Checks whether a command line is valid.
    /// </summary>
    /// <param name="commandLine">The command line to check.</param>
    /// <returns>True if the command line is valid; otherwise, false.</returns>
    public bool IsCommandValid(string commandLine) => new Regex(_options.Rover.Commands.RegexPattern).IsMatch(commandLine);

    /// <summary>
    /// Finds the final position of the rover after executing a string of commands.
    /// </summary>
    /// <param name="commandLine">A string of commands to execute.</param>
    /// <returns>The final position of the rover.</returns>
    public RoverPosition FindCoordinates(string commandLine)
    {
        var currentPosition = new RoverPosition(_options.Rover.Position, _options.Rover.StepSize, _options.Grid);
        for (var i = 0; i < commandLine.Length; i++)
        {
            var command = commandLine[i];
            TryExecuteCommand(i + 1, command, currentPosition);
        }

        return currentPosition;
    }

    /// <summary>
    /// Tries to execute the command corresponding to the given character for the current direction of the rover.
    /// </summary>
    /// <param name="step">The current step number.</param>
    /// <param name="command">The character representing the command to execute.</param>
    /// <param name="roverPosition">The current position of the rover.</param>
    /// <exception cref="NotSupportedException">Thrown when the given command is not supported for the current direction of the rover.</exception>
    private void TryExecuteCommand(int step, char command, RoverPosition roverPosition)
    {
        if (_actions.TryGetValue((roverPosition.Direction, command), out var action))
        {
            ExecuteCommand(action.execute, action.onAfterExecute, roverPosition, command, step);
        }
        else
        {
            throw new NotSupportedException().UpdateExceptionData(command, step);;
        }
    }

    /// <summary>
    /// Executes the given command on the provided <see cref="RoverPosition"/> and calls the optional <paramref name="onAfterExecute"/> action after executing the command.
    /// </summary>
    /// <param name="execute">The action to execute the command.</param>
    /// <param name="onAfterExecute">The optional action to call after executing the command.</param>
    /// <param name="roverPosition">The current position of the rover.</param>
    /// <param name="command">The command to execute.</param>
    /// <param name="step">The step number of the command in the input string.</param>
    /// <exception cref="Exception">If the command execution fails.</exception>
    private static void ExecuteCommand(
        Action<RoverPosition> execute,
        Action<RoverPosition>? onAfterExecute,
        RoverPosition roverPosition,
        char command,
        int step)
    {
        try
        {
            execute(roverPosition);
            onAfterExecute?.Invoke(roverPosition);
        }
        catch (Exception exception)
        {
            exception.UpdateExceptionData(command, step);
            throw;
        }
    }
    
    /// <summary>
    /// Gets a dictionary containing the actions to perform for each possible command and direction combination.
    /// </summary>
    /// <param name="commands">The commands configuration.</param>
    /// <returns>A dictionary containing the actions to perform for each possible command and direction combination.</returns>
    private static Dictionary<(Direction, char), (Action<RoverPosition> execute, Action<RoverPosition>? onAfterExecute)> GetActions(ICommands commands) =>
        new()
        {
            { (Direction.North, commands.Forward), (p => p.MoveVertically(true), p => p.OnAfterMove()) },
            { (Direction.South, commands.Forward), (p => p.MoveVertically(false), p => p.OnAfterMove()) },
            { (Direction.East, commands.Forward), (p => p.MoveHorizontally(true), p => p.OnAfterMove()) },
            { (Direction.West, commands.Forward), (p => p.MoveHorizontally(false), p => p.OnAfterMove()) },

            { (Direction.North, commands.Backwards), (p => p.MoveVertically(false), p => p.OnAfterMove()) },
            { (Direction.South, commands.Backwards), (p => p.MoveVertically(true), p => p.OnAfterMove()) },
            { (Direction.East, commands.Backwards), (p => p.MoveHorizontally(false), p => p.OnAfterMove()) },
            { (Direction.West, commands.Backwards), (p => p.MoveHorizontally(true), p => p.OnAfterMove()) },

            { (Direction.North, commands.Left), (position => position.Turn(Direction.West), null) },
            { (Direction.South, commands.Left), (position => position.Turn(Direction.East), null) },
            { (Direction.East, commands.Left), (position => position.Turn(Direction.North), null) },
            { (Direction.West, commands.Left), (position => position.Turn(Direction.South), null) },

            { (Direction.North, commands.Right), (position => position.Turn(Direction.East), null) },
            { (Direction.South, commands.Right), (position => position.Turn(Direction.West), null) },
            { (Direction.East, commands.Right), (position => position.Turn(Direction.South), null) },
            { (Direction.West, commands.Right), (position => position.Turn(Direction.North), null) }
        };
}