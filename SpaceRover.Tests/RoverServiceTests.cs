using Microsoft.Extensions.Options;
using SpaceRover.ApplicationServices;
using SpaceRover.Enums;
using SpaceRover.Models;

namespace SpaceRover.Tests;

[TestClass]
public class RoverServiceTests
{
    private readonly RoverService _roverService;
    
    public RoverServiceTests()
    {
        var options = Options.Create(Settings.SpaceRoverConfiguration);
        _roverService = new RoverService(options);
    }

    [TestMethod]
    [DataRow("FFX", 'X', 3)]
    [DataRow("XFF", 'X', 1)]
    [DataRow("XXX", 'X', 1)]
    [DataRow("FXF", 'X', 2)]
    public void FindCoordinates_InvalidCommand_ThrowsNotSupportedException(string commands, char invalidCommand, int step)
    {
        var exception = Assert.ThrowsException<NotSupportedException>(() => _roverService.FindCoordinates(commands));
        AssertExceptionData(exception, invalidCommand, step);
    }

    [TestMethod]
    [DataRow("FFFFFF", 'F', 6)]
    [DataRow("B", 'B', 1)]
    [DataRow("FRFFFFFF", 'F', 8)]
    [DataRow("FLF", 'F', 3)]
    public void FindCoordinates_InvalidOperation_ThrowsInvalidOperationExceptionException(string commands, char invalidCommand, int step)
    {
        var exception = Assert.ThrowsException<InvalidOperationException>(() => _roverService.FindCoordinates(commands));
        AssertExceptionData(exception, invalidCommand, step);
    }

    [TestMethod]
    [DataRow("L", 0, 0, Direction.West)]
    [DataRow("LL", 0, 0, Direction.South)]
    [DataRow("LLL", 0, 0, Direction.East)]
    [DataRow("LLLL", 0, 0, Direction.North)]
    [DataRow("R", 0, 0, Direction.East)]
    [DataRow("RR", 0, 0, Direction.South)]
    [DataRow("RRR", 0, 0, Direction.West)]
    [DataRow("RRRR", 0, 0, Direction.North)]
    [DataRow("F", 0, 1, Direction.North)]
    [DataRow("RF", 1, 0, Direction.East)]
    [DataRow("FRRF", 0, 0, Direction.South)]
    [DataRow("RFRRF", 0, 0, Direction.West)]
    [DataRow("FB", 0, 0, Direction.North)]
    [DataRow("RFB", 0, 0, Direction.East)]
    [DataRow("RRB", 0, 1, Direction.South)]
    [DataRow("LB", 1, 0, Direction.West)]
    [DataRow("FFRFF", 2, 2, Direction.East)]
    [DataRow("FFFRFFFRFBBB", 3, 5, Direction.South)]
    [DataRow("FFRFFLFFLBB", 4, 4, Direction.West)]
    public void FindCoordinates_ValidCommands_ReturnsPosition(string command, double x, double y, Direction direction)
    {
        var actual = _roverService.FindCoordinates(command);

        var expected = new RoverPosition(
            new Position(x, y, direction),
            Settings.SpaceRoverConfiguration.Rover.StepSize,
            Settings.SpaceRoverConfiguration.Grid);

        Assert.AreEqual(expected, actual);
    }

    private static void AssertExceptionData(Exception exception, char command, int step)
    {
        Assert.AreEqual(exception.Data[Constants.ExceptionKeys.Command], command);
        Assert.AreEqual(exception.Data[Constants.ExceptionKeys.Step], step);
    }
}