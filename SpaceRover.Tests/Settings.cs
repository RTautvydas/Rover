using SpaceRover.Configuration;
using SpaceRover.Enums;
using SpaceRover.Models;

namespace SpaceRover.Tests;

public static class Settings
{
    public static readonly SpaceRoverConfiguration SpaceRoverConfiguration = new()
    {
        Grid = new Grid
        {
            Height = 5,
            Width = 5
        },
        Rover = new Rover
        {
            Commands = new Commands
            {
                Backwards = 'B',
                Forward = 'F',
                Left = 'L',
                Right = 'R'
            },
            Position = new Position(0, 0, Direction.North),
            StepSize = 1
        }
    };
}