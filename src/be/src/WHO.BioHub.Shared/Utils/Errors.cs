using FluentValidation.Results;

namespace WHO.BioHub.Shared.Utils;

public enum ErrorType : int
{
    NotFound = 0,
    Internal = 1,
    RequestParsing = 2,
    Validation = 3,
    Unauthorized = 4,
    Forbidden = 5
}

public record struct Errors(Dictionary<string, List<string>> Messages, ErrorType ErrorType)
{
    public Errors(ValidationResult result)
        : this(GetMessages(result), ErrorType.Validation) { }

    public Errors(ErrorType errorType, params string[] messages)
    : this(GetMessages(messages), errorType) { }

    private static Dictionary<string, List<string>> GetMessages(ValidationResult result)
    {
        Dictionary<string, List<string>> messages = new();
        foreach (ValidationFailure? error in result.Errors)
        {
            if (error != null)
                AddMessage(messages, error);
        }
        return messages;
    }

    private static Dictionary<string, List<string>> GetMessages(string[] errors)
    {
        Dictionary<string, List<string>> messages = new();
        foreach (string message in errors)
        {
            AddMessage(messages, message);
        }

        return messages;
    }

    private static void AddMessage(Dictionary<string, List<string>> messages, ValidationFailure error)
    {
        if (!messages.ContainsKey(error.PropertyName))
        {
            messages[error.PropertyName] = new List<string>() { error.ErrorMessage };
            return;
        }

        messages[error.PropertyName].Add(error.ErrorMessage);
    }

    private static void AddMessage(Dictionary<string, List<string>> messages, string message)
    {
        if (!messages.ContainsKey(message))
        {
            messages[message] = new List<string>() { message };
            return;
        }

        messages[message].Add(message);
    }
}

public static class ValidationResultExtensions
{
    public static Errors ToErrors(this ValidationResult result) => new(result);
}