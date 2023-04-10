using Microsoft.AspNetCore.Mvc;
using Resources;
using SpaceRover.ApplicationServices;
using SpaceRover.DTOs;
using SpaceRover.Helpers;
using SpaceRover.Models;

namespace SpaceRover.Controllers;

/// <summary>
/// A controller that handles requests related to a space rover.
/// </summary>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class RoverController : ControllerBase
{
    private readonly IRoverService _roverService;

    public RoverController(IRoverService roverService)
    {
        _roverService = roverService;
    }
    
    /// <summary>
    /// Handles a request to move a space rover.
    /// </summary>
    /// <param name="commands">The commands to move the rover.</param>
    /// <returns>An action result representing the current position of the rover.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoverPosition))]
    public IActionResult SendCommands(RoverCommandDto commands)
    {
        if (!_roverService.IsCommandValid(commands.CommandsLine))
        {
            return BadRequest(Resource.CommandContainsInvalidSymbolsError);
        }
        
        try
        {
            return Ok(_roverService.FindCoordinates(commands.CommandsLine));
        }
        catch (Exception ex)
        {
            var message = ex.Message
                .AppendAdditionalData(ex, Constants.ExceptionKeys.Command)
                .AppendAdditionalData(ex, Constants.ExceptionKeys.Step);
            
            return BadRequest(message);
        }
    }
}