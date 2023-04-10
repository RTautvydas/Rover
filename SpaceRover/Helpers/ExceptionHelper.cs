namespace SpaceRover.Helpers;

/// <summary>
/// Exceptions helper
/// </summary>
public static class ExceptionHelper
{
    /// <summary>
    /// Appends additional data from the specified key-value pair of the exception's <see cref="Exception.Data"/> dictionary
    /// to the specified error message.
    /// </summary>
    /// <param name="message">The error message to append to. If <c>null</c>, the exception message will be used instead.</param>
    /// <param name="exception">The exception that contains the additional data.</param>
    /// <param name="key">The key of the additional data to append to the error message.</param>
    /// <returns>The error message with the additional data appended, if it exists in the exception's <see cref="Exception.Data"/> dictionary.</returns>
    public static string AppendAdditionalData(this string? message, Exception exception, string key)
    {
        message ??= exception.Message;
        if (exception.Data.Contains(key))
        {
            var value = exception.Data[key]?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                message += $" {key}: {value}" ;
            }
        }

        return message;
    }
    
    /// <summary>
    /// Updates the data of the specified exception with the command and step information.
    /// </summary>
    /// <typeparam name="T">The type of the exception to update.</typeparam>
    /// <param name="exception">The exception to update.</param>
    /// <param name="command">The command that caused the exception.</param>
    /// <param name="step">The step at which the exception occurred.</param>
    /// <returns>The updated exception.</returns>
    public static T UpdateExceptionData<T>(this T exception, char command, int step) where T : Exception
    {
        exception.Data.Add(Constants.ExceptionKeys.Command, command);
        exception.Data.Add(Constants.ExceptionKeys.Step, step);

        return exception;
    }
}