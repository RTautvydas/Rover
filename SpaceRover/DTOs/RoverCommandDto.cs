using System.ComponentModel.DataAnnotations;
using Resources;

namespace SpaceRover.DTOs;

/// <summary>
/// Rover commands DTO
/// </summary>
public class RoverCommandDto
{
    /// <summary>
    /// List of commands
    /// </summary>
    [Required(
        AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resource),
        ErrorMessageResourceName = nameof(Resource.EmptyCommandError))]
    public string CommandsLine { get; set; }
}