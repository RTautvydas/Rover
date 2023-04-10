using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Resources;
using SpaceRover.ApplicationServices;
using SpaceRover.Controllers;
using SpaceRover.DTOs;
using SpaceRover.Models;

namespace SpaceRover.Tests;

[TestClass]
public class RoverControllerTests
{
    private readonly Mock<IRoverService> _mockRoverService;
    private readonly RoverController _roverController;

    public RoverControllerTests()
    {
        _mockRoverService = new Mock<IRoverService>();
        _roverController = new RoverController(_mockRoverService.Object);
    }
    
    [TestMethod]
    public void SendCommands_WithValidCommand_ReturnsOkObjectResult()
    {
        var commands = new RoverCommandDto { CommandsLine = "LLR" };
        var expectedCoordinates = new RoverPosition(Settings.SpaceRoverConfiguration.Rover.Position,
            Settings.SpaceRoverConfiguration.Rover.StepSize,
            Settings.SpaceRoverConfiguration.Grid);
        
        _mockRoverService.Setup(x => x.IsCommandValid(commands.CommandsLine)).Returns(true);
        _mockRoverService.Setup(x => x.FindCoordinates(commands.CommandsLine)).Returns(expectedCoordinates);

        var result = _roverController.SendCommands(commands) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        Assert.AreEqual(expectedCoordinates, result.Value);
    }
    
    [TestMethod]
    public void SendCommands_WithInvalidCommand_ReturnsBadRequestObjectResult()
    {
        var commands = new RoverCommandDto { CommandsLine = "XXX" };
        _mockRoverService.Setup(x => x.IsCommandValid(commands.CommandsLine)).Returns(false);

        var result = _roverController.SendCommands(commands) as BadRequestObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.AreEqual(Resource.CommandContainsInvalidSymbolsError, result.Value);
    }
    
    [TestMethod]
    public void SendCommands_WithException_ReturnsBadRequestObjectResultWithAdditionalData()
    {
        var commands = new RoverCommandDto { CommandsLine = "FFBBBB" };
        
        var options = Options.Create(Settings.SpaceRoverConfiguration);
        var service = new RoverService(options);

        var roverController = new RoverController(service);
        var result = roverController.SendCommands(commands) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.IsTrue(result.Value.ToString().Contains($"{Constants.ExceptionKeys.Command}: B"));
        Assert.IsTrue(result.Value.ToString().Contains($"{Constants.ExceptionKeys.Step}: 5"));
    }
}