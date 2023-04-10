# Space Rover

Space Rover is a project that simulates the movements of a rover in a rectangular grid.

The project consists of several components:

- `Rover`: A class representing the rover with its initial position and movement capabilities.
- `Grid`: A class representing the rectangular grid with its boundaries.
- `Commands`: A class representing the available commands that the rover can perform, such as move forward or turn left.
- `RoverService`: A service that handles the execution of the commands and calculates the final position of the rover.
- `RoverController`: A controller that exposes an HTTP API for interacting with the `RoverService`.

## Getting Started

### Prerequisites

To run this project, you'll need:

- .NET 5.0 SDK or later

### Running the project

To run the project, open a terminal in the root folder and execute the following commands:

```sh
cd SpaceRover
dotnet run
```

This will start the API server, which can be accessed at http://localhost:5000.

### Running tests

To run the tests, open a terminal in the root folder and execute the following command:

```sh
cd SpaceRover.Tests
dotnet test
```

This will run all the tests in the solution.

## API Reference

The API exposes a single endpoint:

### `POST /api/rover`

This endpoint expects a JSON payload with the following structure:

```json
{
  "commandsLine": "FFFLLRBB"
}
```

The `commandsLine` property should contain a string with a sequence of commands that the rover should execute. The available commands are:

- `F`: Move one step forward in the current direction.
- `B`: Move one step backward in the current direction.
- `L`: Turn 90 degrees to the left.
- `R`: Turn 90 degrees to the right.

The endpoint returns a JSON payload with the following structure:

```json
{
  "x": 3.0,
  "y": 4.0,
  "direction": "N"
}
```

The `x` and `y` properties represent the final coordinates of the rover, and the `direction` property represents the final direction that the rover is facing. The direction can be one of the following values: `N` (north), `E` (east), `S` (south), or `W` (west).

If the `commandsLine` property contains invalid characters, the API returns a `400 Bad Request` response with the following payload:

```json
{
  "error": "The commands line contains invalid symbols."
}
```

If an error occurs during the execution of the commands, the API returns a `400 Bad Request` response with the following payload:

```json
{
  "error": "An error occurred during the execution of the commands.",
  "command": "FFFLLRBB",
  "step": 5
}
```

The `command` property contains the sequence of commands that was being executed when the error occurred, and the `step` property contains the index of the step that caused the error.
